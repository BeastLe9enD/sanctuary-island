using Inventory;
using Story;
using UI;
using UnityEngine;
using Utils;

namespace Animals.Kangaroos
{
    public class KangarooTradeState : IAnimalState
    {
        private ItemRegistry _itemRegistry;
        private PlayerInventory _playerInventory;
        private StoryManager _storyManager;
        private PopupManager _popupManager;
        
        private bool _hasSeedFeed, _hasBerryFeed;

        public void OnEnter(AnimalStateManager manager)
        {
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();
            _storyManager = Object.FindObjectOfType<StoryManager>();
            _popupManager = Object.FindObjectOfType<PopupManager>();

            _hasSeedFeed = false;
            _hasBerryFeed = false;
        }

        private void HandleFood(ref bool value, StackedItem stack)
        {
            if (value || !_playerInventory.CanRemove(stack))
            {
                return;
            }

            _playerInventory.Remove(stack);

            value = true;
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            if (_hasSeedFeed && _hasBerryFeed)
            {
                ItemDropUtils.DropItems(manager.transform.position, new StackedItem(_itemRegistry.Cake), 2.0f);

                if (!_storyManager.FirstCakeCrafted)
                {
                    _storyManager.FirstCakeCrafted = true;
                    
                    _popupManager.Enqueue("Yaaay, you got your first cake!");
                    _popupManager.Enqueue("You can give the cake to the mole.");
                    _popupManager.Enqueue("It will build a second pond where the flamingo will arrive on the next day.");
                }
                
                manager.Switch<AnimalTamedState>();
            }
        }
        
        public void OnMouseOver(AnimalStateManager manager)
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }

            HandleFood(ref _hasSeedFeed, new StackedItem(_itemRegistry.SeedFeed));
            HandleFood(ref _hasBerryFeed, new StackedItem(_itemRegistry.BerryFeed));
        }
    }
}