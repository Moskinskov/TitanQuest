using UnityEngine;

namespace Assets.Scripts
{
    public class PickUpItem : Interactable
    {
        public Item item;
        public override bool Interact(GameObject user)
        {
            return PickUp(user);
        }

        private bool PickUp(GameObject user)
        {
            Character character = user.GetComponent<Character>();
            if (character != null && character._inventory.Add(item))
            {
                Destroy(gameObject);
                return true;
            }
            return false;
        }
    }
}