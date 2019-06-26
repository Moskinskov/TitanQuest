using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class Inventory : NetworkBehaviour
    {
        public Transform dropPoint;
        public int space = 20;
        public SyncListItem items = new SyncListItem();
        public event SyncList<Item>.SyncListChanged onItemChanged;

        public override void OnStartLocalPlayer()
        {
            items.Callback += ItemChanged;
        }

        private void ItemChanged(SyncList<Item>.Operation op, int itemIndex)
        {
            onItemChanged(op, itemIndex);
        }
        public bool Add(Item item)
        {
            if (items.Count < space)
            {
                items.Add(item);
                return true;
            }
            else return false;
        }

        public void Remove(Item item)
        {
            CmdRemoveItem(items.IndexOf(item));
        }

        [Command]
        private void CmdRemoveItem(int index)
        {
            if (items[index] != null)
            {
                Drop(items[index]);
                items.RemoveAt(index);
            }
        }
        private void Drop(Item item)
        {
            PickUpItem pickupItem = Instantiate(item.item, dropPoint.position,
                Quaternion.Euler(0, Random.Range(0, 360f), 0));
            pickupItem.item = item;
            NetworkServer.Spawn(pickupItem.gameObject);
        }
    }
}