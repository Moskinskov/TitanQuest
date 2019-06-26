using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item : ScriptableObject
    {
        public new string name = "New item";
        public Sprite icon = null;
        public PickUpItem item;

        public virtual void Use()
        {
            Debug.Log("Using " + name);
        }
    }
}