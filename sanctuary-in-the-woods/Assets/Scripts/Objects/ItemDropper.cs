using Inventory;
using UnityEngine;
using Utils;

namespace Objects {
    public class ItemDropper : MonoBehaviour {
        public Item Item;
        public int Count;
        public float DropRadius;

        public void DropItems()
        {
            ItemDropUtils.DropItems(transform.position, new StackedItem(Item, Count), DropRadius);
        }
    }
}