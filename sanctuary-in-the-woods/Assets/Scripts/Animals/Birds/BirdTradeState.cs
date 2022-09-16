using Inventory;
using UnityEngine;
using Utils;

namespace Animals.Birds
{
    public sealed class BirdTradeState : IAnimalState
    {
        private ItemRegistry _itemRegistry;
        private PlayerInventory _playerInventory;

        private float _startTime;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();

            _startTime = Time.deltaTime;
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            if (Time.time - _startTime >= 2.0)
            {
                ItemDropUtils.DropItems(manager.transform.position, new StackedItem(_itemRegistry.Seeds), 2.0f);
                manager.Switch<AnimalTamedState>();
            }
        }
    }
}