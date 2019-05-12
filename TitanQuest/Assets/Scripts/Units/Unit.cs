using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class Unit : NetworkBehaviour
    {
        [SerializeField] protected UnitStats stats;
        [SerializeField] protected UnitMotor motor;
        [SerializeField] protected bool isDead;

        private void Update()
        {
            OnUpdate();
        }

        private void OnUpdate()
        {
            if (isServer)
            {
                if (!isDead)
                {
                    if (stats.CurrentHealth <= 0)
                        Die();
                    else
                        OnAliveUpdate();
                }
                else
                    OnDeadUpdate();
            }
        }

        protected virtual void OnAliveUpdate() { }
        protected virtual void OnDeadUpdate() { }

        #region Client's methods
        /// <summary>
        /// 'Core' Смерть
        /// </summary>
        protected virtual void Die()
        {
            isDead = true;
            if (isServer)
            {
                motor.MoveToPoint(transform.position);
                RpcDie();
            }
        }
        /// <summary>
        /// 'Core' Возрождение
        /// </summary>
        protected virtual void Revive()
        {
            isDead = false;
            if (isServer)
            {
                stats.SetHealthRate(1);
                RpcRevive();
            }
        }

        #endregion

        #region Server's methods
        
        [ClientRpc]
        private void RpcDie()
        {
            if (!isServer)
                Die();
        }
        [ClientRpc]
        private void RpcRevive()
        {
            if (!isServer)
                Revive();
        }

        #endregion
    }
}