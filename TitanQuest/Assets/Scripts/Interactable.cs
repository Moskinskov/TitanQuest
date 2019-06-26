using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class Interactable : NetworkBehaviour
    {
        public Transform interactionTransform;
        public float radius = 2f;

        private bool hasInteract = true;

        private void OnValidate()
        {
            interactionTransform = GetComponent<Transform>();
        }

        public bool HasInteract
        {
            get { return hasInteract; }
            set { hasInteract = value; }
        }
        public virtual bool Interact(GameObject user)
        {
            return false;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(interactionTransform.position, radius);
        }

    }
}