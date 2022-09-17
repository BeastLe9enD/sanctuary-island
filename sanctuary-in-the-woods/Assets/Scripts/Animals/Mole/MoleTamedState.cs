using Inventory;
using Story;
using UnityEngine;

namespace Animals.Mole
{
    public sealed class MoleTamedState : IAnimalState
    {
        private Player _player;
        private ItemRegistry _itemRegistry;
        private PlayerInventory _playerInventory;
        private StoryManager _storyManager;
        
        private bool _shouldFollow;
        private float _lastUpdateTime;

        public void OnEnter(AnimalStateManager manager)
        {
            _shouldFollow = false;
            _player = Object.FindObjectOfType<Player>();
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();
            _storyManager = Object.FindObjectOfType<StoryManager>();
        }

        public void OnUpdate(AnimalStateManager manager)
        {
            if (!_shouldFollow)
            {
                return;
            }

            if (Time.time - _lastUpdateTime >= 0.5)
            {
                _lastUpdateTime = Time.time;
                manager.Agent.SetDestination(_player.transform.position);
            }
        }

        public void OnMouseOver(AnimalStateManager manager)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_storyManager.PondPlaced)
                {
                    var stack = new StackedItem(_itemRegistry.SeedFeed, 6);
                    
                    if (!_playerInventory.CanRemove(stack))
                    {
                        return;
                    }

                    _playerInventory.Remove(stack);
                    manager.Switch<MolePondBuildState>();
                }
                else if (!_storyManager.SecondPondPlaced)
                {
                    var stack = new StackedItem(_itemRegistry.Cake);
                    
                    if (!_playerInventory.CanRemove(stack))
                    {
                        return;
                    }

                    _playerInventory.Remove(stack);
                    manager.Switch<MolePondBuildState>();
                }

                return;
            }
            if (Input.GetMouseButtonDown(1))
            {
                _shouldFollow = !_shouldFollow;
            }
        }
    }
}