using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class UnitStats : NetworkBehaviour
    {
        [SerializeField] private int maxHealth;
        [SyncVar] private int currentHealth;
        [SerializeField] private Stat damage;


        public int CurrentHealth
        {
            get { return currentHealth; }
        }

        public Stat Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public void SetHealthRate(float rate)
        {
            currentHealth = rate == 0 ? 0 : (int)(maxHealth / rate);
        }

        public override void OnStartServer()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
    }
}