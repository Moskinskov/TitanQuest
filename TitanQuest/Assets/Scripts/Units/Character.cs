using UnityEngine;

namespace Assets.Scripts
{
    public class Character : Unit
    {
        [SerializeField] private float reviveDelay = 5f;
        [SerializeField] private GameObject gfx;
        private Vector3 startPosition;
        private float reviveTime;

        public Inventory _inventory;

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

        protected override void OnAliveUpdate()
        {
            base.OnAliveUpdate();
            if (focusObject != null)
            {
                if (!focusObject.HasInteract)
                {
                    RemoveFocus();
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

        public void SetNewFocus(Interactable newFocus)
        {
            if (!isDead)
            {
                if (newFocus.HasInteract)
                {
                    SetFocus(newFocus);
                }
            }
        }

        public void SetInventory(Inventory inventory)
        {
            _inventory = inventory;
            inventory.dropPoint = transform;
        }

        #endregion
    }
}