using System.Linq;
using Inventory;
using Objects;
using UnityEngine;

namespace Animals.Rabbits
{
    public class RabbitTreeCutState : IAnimalState
    {
        private ItemRegistry _itemRegistry;
        private PlayerInventory _playerInventory;

        private TreeStorage _targetTree;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();

            _targetTree = Object.FindObjectsOfType<TreeStorage>()
                .OrderBy(tree => Vector3.Distance(tree.transform.position, manager.transform.position))
                .First();

            manager.Agent.SetDestination(_targetTree.transform.position);
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            if (manager.Agent.remainingDistance <= 1.0)
            {
                _targetTree.GetComponent<ItemDropper>().DropItems();
                Object.Destroy(_targetTree.gameObject);
                
                manager.Switch<AnimalTamedState>();
            }
        }
    }
}