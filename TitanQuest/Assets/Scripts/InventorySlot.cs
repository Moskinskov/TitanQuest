using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InventorySlot : MonoBehaviour
    {
        public Image icon;
        public Button removeButton;
        public Inventory inventory;
        private Item item;

        public void SetItem(Item newItem)
        {
            item = newItem;
            icon.sprite = item.icon;
            icon.enabled = true;
            removeButton.interactable = true;
        }

        public void ClearSlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            removeButton.interactable = false;
        }

        public void OnRemoveButton()
        {
            inventory.Remove(item);
        }

        public void UseItem()
        {
            if (item != null) item.Use();
        }
    }
}