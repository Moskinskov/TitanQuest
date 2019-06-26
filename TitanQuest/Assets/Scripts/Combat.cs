using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    [RequireComponent(typeof(UnitStats))]
    public class Combat : NetworkBehaviour
    {
        [SerializeField] private float attackSpeed = 1f;
        private float attackCoolDown = 0f;
        private UnitStats stats;


        [SyncEvent] public event Action EventOnAttack;

        private void Start()
        {
            stats = GetComponent<UnitStats>();
        }

        private void Update()
        {
            if (attackCoolDown > 0)
                attackCoolDown -= Time.deltaTime;
        }

        public bool Attack(UnitStats targetStats)
        {
            print(gameObject.name + " attacking " + targetStats.gameObject.name);
            if (attackCoolDown <= 0)
            {
                targetStats.TakeDamage(stats.Damage.GetValue);
                EventOnAttack?.Invoke();
                attackCoolDown = 1 / attackSpeed;
                return true;
            }

            return false;
        }
    }
}