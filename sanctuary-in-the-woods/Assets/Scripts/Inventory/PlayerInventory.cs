using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [ItemCanBeNull] public readonly StackedItem[] slots = new StackedItem[10];

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
                var image = slot.GetChild(0).GetChild(0).GetComponent<Image>();
                
                var item = slots[index++];
                if (item == null)
                {
                    image.enabled = false;
                    image.sprite = null;
                }
                else
                {
                    image.enabled = true;
                    image.sprite = image.sprite;
                }
            }
        }

        public bool CanAdd(StackedItem stack)
        {
            return false; //TODO:
        }

        public void Add(StackedItem stack)
        {
            //TODO:
        }
        
        public bool CanRemove(StackedItem stack)
        {
            return false; //TODO:
        }

        public void Remove(StackedItem stack)
        {
            //TODO:
        }
    }
}