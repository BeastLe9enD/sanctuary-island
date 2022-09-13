using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    /// <summary>
    /// Represents the inventory of the player, with all items inside
    /// </summary>
    public class PlayerInventory : MonoBehaviour
    {
        [ItemCanBeNull] public readonly StackedItem[] Slots = new StackedItem[10];

        void Start()
        {
            UpdateUI();
        }
        
        private void UpdateUI()
        {
            var panel = GameObject.Find("PlayerInventoryPanel");

            var index = 0;
            foreach (Transform slot in panel.transform)
            {
                var child = slot.GetChild(0).GetChild(0);
                
                var image = child.GetComponent<Image>();
                var text = child.GetChild(0).GetComponent<Text>();
                
                var item = Slots[index++];
                if (item == null)
                {
                    image.enabled = false;
                    image.sprite = null;
                    text.enabled = false;
                }
                else
                {
                    image.enabled = true;
                    image.sprite = item.Item.Sprite;
                    text.enabled = item.Count > 1;

                    if (text.enabled)
                    {
                        text.text = item.Count.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        /// <returns></returns>
        public bool CanAdd(StackedItem stack)
        {
            var maxStackSize = stack.Item.MaxStackSize;
            var remainingSize = stack.Count;

            foreach (var slot in Slots)
            {
                if (slot == null || slot.Item != stack.Item) continue;
                
                var requestedSize = remainingSize + slot.Count;
                if (requestedSize > maxStackSize)
                {
                    remainingSize = requestedSize - maxStackSize;
                }
                else
                {
                    remainingSize = 0;
                }

                if (remainingSize == 0)
                {
                    break;
                }
            }

            if (remainingSize <= 0) return false;
            {
                foreach (var slot in Slots)
                {
                    if (slot == null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Adds an stack from the inventory. Assume that the stack can be added to the inventory
        /// </summary>
        /// <param name="stack"></param>
        public void Add(StackedItem stack)
        {
            var maxStackSize = stack.Item.MaxStackSize;
            var remainingSize = stack.Count;

            foreach (var slot in Slots)
            {
                if (slot == null || slot.Item != stack.Item) continue;
                var requestedSize = remainingSize + slot.Count;
                if (requestedSize > maxStackSize)
                {
                    slot.Count = maxStackSize;
                    remainingSize = requestedSize - maxStackSize;
                }
                else
                {
                    slot.Count = requestedSize;
                    remainingSize = 0;
                }

                if (remainingSize == 0)
                {
                    break;
                }
            }
            
            if (remainingSize > 0)
            {
                for (var i = 0; i < Slots.Length; i++)
                {
                    if (Slots[i] == null)
                    {
                        Slots[i] = new StackedItem(stack.Item, remainingSize);
                        break;
                    }
                }
            }

            UpdateUI();
        }
        
        /// <summary>
        /// Check if the stack is present in the inventory and can be removed
        /// </summary>
        /// <param name="stack">The stack to remove</param>
        /// <returns>If the stack can be removed</returns>
        public bool CanRemove(StackedItem stack)
        {
            var remainingSize = stack.Count;

            foreach (var slot in Slots)
            {
                if (slot == null || slot.Item != stack.Item) continue;
                if (slot.Count > remainingSize)
                {
                    return true;
                }
                remainingSize -= slot.Count;
            }

            return remainingSize == 0;
        }

        /// <summary>
        /// Removes an stack from the inventory. Assume that the stack can be removed from the inventory
        /// </summary>
        /// <param name="stack">The stack to remove</param>
        public void Remove(StackedItem stack)
        {
            var remainingSize = stack.Count;

            for (var i = 0; i < Slots.Length; i++)
            {
                var slot = Slots[i];
                if (slot == null || slot.Item != stack.Item) continue;
                if (slot.Count > remainingSize)
                {
                    slot.Count -= remainingSize;
                    UpdateUI();
                    
                    return;
                }
                
                remainingSize -= slot.Count;
                Slots[i] = null;
            }
            
            UpdateUI();
        }
    }
}