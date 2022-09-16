using Inventory;
using UnityEngine;

namespace Animals.Birds
{
    public sealed class BirdTradeState : IAnimalState
    {
        private ItemRegistry _itenRegistry;
        private PlayerInventory _playerInventory;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _itenRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();
        }

        public void OnUpdate(AnimalStateManager manager)
        {
            throw new System.NotImplementedException();
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            throw new System.NotImplementedException();
        }

        public void OnCollisionEnter(AnimalStateManager manager, Collision2D collision)
        {
            throw new System.NotImplementedException();
        }

        public void OnMouseOver(AnimalStateManager manager)
        {
            throw new System.NotImplementedException();
        }
    }
}