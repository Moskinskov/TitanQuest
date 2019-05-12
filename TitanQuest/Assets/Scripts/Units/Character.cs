using UnityEngine;

namespace Assets.Scripts
{
    public class Character : Unit
    {
        [SerializeField] private float reviveDelay = 5f;
        [SerializeField] private GameObject gfx;
        private Vector3 startPosition;
        private float reviveTime;

        private void Start()
        {
            startPosition = transform.position;
            reviveTime = reviveDelay;
        }

        #region Overrided methods
        /// <summary>
        /// Player's 'OnDeadUpdate'
        /// </summary>
        protected override void OnDeadUpdate()
        {
            base.OnDeadUpdate();
            if (reviveTime > 0)
                reviveTime -= Time.deltaTime;
            else
            {
                reviveTime = reviveDelay;
                Revive();
            }
        }
        /// <summary>
        /// Player's 'Revive'
        /// </summary>
        protected override void Revive()
        {
            base.Revive();
            transform.position = startPosition;
            gfx.SetActive(true);
            if (isServer)
            {
                motor.MoveToPoint(startPosition);
            }

        }
        /// <summary>
        /// Player's 'Die'
        /// </summary>
        protected override void Die()
        {
            base.Die();
            gfx.SetActive(false);
        }
        #endregion

        #region Public methods

        public void SetMovePoint(Vector3 point)
        {
            if (!isDead)
                motor.MoveToPoint(point);
        }

        #endregion
    }
}