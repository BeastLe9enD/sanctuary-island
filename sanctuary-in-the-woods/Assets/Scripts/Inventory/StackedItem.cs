using System;

namespace Inventory
{
    /// <summary>
    /// An item with a size
    /// </summary>
    
    [Serializable]
    public sealed class StackedItem
    {
        public Item Item;
        public int Count;

        public StackedItem(Item item, int count = 1)
        {
            Item = item;
            Count = count;
        }

        public void Increment(int size = 1) => Count += size;
        public void Decrement(int size = 1) => Count -= size;
    }
}