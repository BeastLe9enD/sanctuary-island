using System.Linq;
using Inventory;
using Objects;
using UnityEngine;

namespace Animals.Elephants
{
    public sealed class ElephantDestroyRockState : IAnimalState
    {
        private ItemRegistry _itemRegistry;
        private PlayerInventory _playerInventory;

        private RockStorage _rockStorage;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();

            var objects = Object.FindObjectsOfType<RockStorage>();
            if (objects.Length == 0)
            {
                manager.Switch<AnimalTamedState>();
                return;
            }
            
            _rockStorage = Object.FindObjectsOfType<RockStorage>()
                .OrderBy(tree => Vector3.Distance(tree.transform.position, manager.transform.position))
                .First();

            manager.Agent.SetDestination(_rockStorage.transform.position);
        }

        public void OnMouseOver(AnimalStateManager manager)
        {
            Debug.Log(manager.Agent.remainingDistance);
            if (manager.Agent.remainingDistance <= 40.0)
            {
                _rockStorage.GetComponent<ItemDropper>().DropItems();
                Object.Destroy(_rockStorage.gameObject);
                
                manager.Switch<AnimalTamedState>();
            }
        }
    }
}