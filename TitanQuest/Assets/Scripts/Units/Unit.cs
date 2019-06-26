using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class Unit : Interactable
    {
        [SerializeField] protected UnitStats stats;
        [SerializeField] protected UnitMotor motor;
        [SerializeField] protected bool isDead;
        protected Interactable focusObject;

        [SyncEvent] public event Action EventOnDamage;
        [SyncEvent] public event Action EventOnDie;
        [SyncEvent] public event Action EventOnRevive;

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

        #region Virtual methods

        protected virtual void OnAliveUpdate()
        {
        }

        protected virtual void OnDeadUpdate()
        {
        }

        protected virtual void SetFocus(Interactable newFocus)
        {
            if (newFocus != focusObject)
            {
                focusObject = newFocus;
                motor.FollowTarget(newFocus);
            }
        }

        protected virtual void RemoveFocus()
        {
            focusObject = null;
            motor.StopFollowingTarget();
        }

        #endregion

        #region Client's methods

        /// <summary>
        /// 'Core' Смерть
        /// </summary>
        [ClientCallback]
        protected virtual void Die()
        {
            isDead = true;
            if (isServer)
            {
                HasInteract = false;
                motor.MoveToPoint(transform.position);
                RpcDie();
                RemoveFocus();
                EventOnDie?.Invoke();
            }
        }

        /// <summary>
        /// 'Core' Возрождение
        /// </summary>
        [ClientCallback]
        protected virtual void Revive()
        {
            isDead = false;
            if (isServer)
            {
                HasInteract = true;
                stats.SetHealthRate(1);
                RpcRevive();
                EventOnRevive?.Invoke();
            }
        }

        #endregion

        #region Server's methods

        [ClientRpc]
        private void RpcDie()
        {
            if (!isServer)
            {
                Die();
            }
        }

        [ClientRpc]
        private void RpcRevive()
        {
            if (!isServer)
                Revive();
        }

        #endregion

        #region Overrides methods

        public override bool Interact(GameObject user)
        {
            Combat tempCombat = user.GetComponentInParent<Combat>();

            if (tempCombat != null)
            {
                if (tempCombat.Attack(stats))
                {
                    EventOnDamage?.Invoke();
                }
                return true;
            }

            return base.Interact(user);
        }

        #endregion
    }
}