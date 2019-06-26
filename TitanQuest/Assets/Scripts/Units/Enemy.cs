using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(UnitMotor), typeof(EnemyStats))]
    public class Enemy : Unit
    {
        [SerializeField, Header("Enemy movement properties")]
        private float moveRadius = 10f;

        [SerializeField] private float minMoveDelay = 4f;
        [SerializeField] private float maxMoveDelay = 10f;

        private Vector3 startPosition;
        private Vector3 currentDestination;
        private float changePosTime;

        [SerializeField, Header("Enemy behavior properties")]
        private bool agressive;

        [SerializeField] private float viewDistance = 5f;
        [SerializeField] private float reviveDelay = 5f;
        private float reviveTime;

        private void Start()
        {
            startPosition = transform.position;
            changePosTime = Random.Range(minMoveDelay, maxMoveDelay);
            reviveTime = reviveDelay;
        }

        #region Overrided methods

        /// <summary>
        /// Enemy's 'OnAliveUpdate'
        /// </summary>
        protected override void OnAliveUpdate()
        {
            base.OnAliveUpdate();
            if (focusObject == null)
            {
                Wandering(Time.deltaTime);
                if (agressive)
                    FindEnemy();
            }
            else
            {
                var tempVector = focusObject.interactionTransform.position - transform.position;
                var tempSqrDistance = tempVector.sqrMagnitude;
                if (tempSqrDistance > Mathf.Pow(focusObject.radius, 2) || !focusObject.HasInteract)
                {
                    RemoveFocus();
                }
                else if (tempSqrDistance <= Mathf.Pow(focusObject.radius, 2))
                {
                    if (focusObject.Interact(gameObject))
                        RemoveFocus();
                }
            }
        }

        /// <summary>
        /// Enemy's 'OnDeadUpdate'
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
        /// Enemy's 'Revive'
        /// </summary>
        protected override void Revive()
        {
            base.Revive();
            transform.position = startPosition;
            if (isServer)
                motor.MoveToPoint(startPosition);
        }

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, viewDistance);
        }

        public override bool Interact(GameObject user)
        {
            print(gameObject.name + "interact with" + user.name);
            SetFocus(user.GetComponent<Interactable>());
            return true;
        }

        #endregion

        #region Patrol methods

        /// <summary>
        /// Блуждание
        /// </summary>
        /// <param name="deltaTime">спустя "время"</param>
        private void Wandering(float deltaTime)
        {
            changePosTime -= deltaTime;
            if (changePosTime <= 0)
            {
                RandomMove();
                changePosTime = Random.Range(minMoveDelay, maxMoveDelay);
            }
        }

        /// <summary>
        /// Рандом мув
        /// </summary>
        private void RandomMove()
        {
            currentDestination =
                Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up) * new Vector3(moveRadius, 0, 0) + startPosition;
            motor.MoveToPoint(currentDestination);
        }

        private void FindEnemy()
        {
            Collider[] tempColliders =
                Physics.OverlapSphere(transform.position, viewDistance, 1 << LayerMask.NameToLayer("Player"));
            for (int i = 0; i < tempColliders.Length; i++)
            {
                Interactable interactable = tempColliders[i].GetComponent<Interactable>();
                if (interactable != null && interactable.HasInteract)
                {
                    SetFocus(interactable);
                    break;
                }
            }
        }

        #endregion
    }
}