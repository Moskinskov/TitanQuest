using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "New Item Collection",menuName = "Inventory/Item Collection")]
    public class ItemCollection:ScriptableObject
    {
        public Item[] items=new Item[0];
    }
}