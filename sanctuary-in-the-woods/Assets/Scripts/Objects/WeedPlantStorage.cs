using Inventory;
using UnityEngine;
using Utils;

namespace Objects
{
    public class WeedPlantStorage : MonoBehaviour
    {
        private PlayerInventory _playerInventory;
        private ItemRegistry _itemRegistry;

        void Start()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _itemRegistry = FindObjectOfType<ItemRegistry>();
        }
        
        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            var random = RamdomUtils.GetRandom();
            var stack = new StackedItem(_itemRegistry.Weed, 1 + random.NextInt(2));

            if (!_playerInventory.CanAdd(stack))
            {
                return;
            }

            _playerInventory.Add(stack);
            Destroy(gameObject);
        }
    }
}