using Inventory;
using UnityEngine;

namespace Animals.Rabbits
{
    public class RabbitTreeCutState : IAnimalState
    {
        private ItemRegistry _itemRegistry;
        private PlayerInventory _playerInventory;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();
        }

        public void OnUpdate(AnimalStateManager manager)
        {
            
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            
        }

        public void OnCollisionEnter(AnimalStateManager manager, Collision2D collision)
        {
            
        }

        public void OnMouseOver(AnimalStateManager manager)
        {
            
        }
    }
}