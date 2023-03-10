using System.Linq;
using Inventory;
using Objects;
using Story;
using UI;
using UnityEngine;

namespace Animals.Rabbits
{
    public class RabbitTreeCutState : IAnimalState
    {
        private ItemRegistry _itemRegistry;
        private PlayerInventory _playerInventory;
        private StoryManager _storyManager;
        private PopupManager _popupManager;
        private TodoManager _todoManager;

        private TreeStorage _targetTree;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _playerInventory = Object.FindObjectOfType<PlayerInventory>();
            _storyManager = Object.FindObjectOfType<StoryManager>();
            _popupManager = Object.FindObjectOfType<PopupManager>();
            _todoManager = Object.FindObjectOfType<TodoManager>();

            _targetTree = Object.FindObjectsOfType<TreeStorage>()
                .OrderBy(tree => Vector3.Distance(tree.transform.position, manager.transform.position))
                .First();

            manager.Agent.SetDestination(_targetTree.transform.position);
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            if (manager.Agent.remainingDistance <= 1.0)
            {
                var stump = _targetTree.Stump;
                var treePosition = _targetTree.transform.position;
                
                _targetTree.GetComponent<ItemDropper>().DropItems();
                Object.Destroy(_targetTree.gameObject);

                Object.Instantiate(stump, treePosition, Quaternion.identity);

                if (!_storyManager.FirstTreeCutDown)
                {
                    _popupManager.Enqueue("With 10 wood and 2 weed, you can build a bird house.");
                    _popupManager.Enqueue("You can place objects with the right mouse button.");
                    _popupManager.Enqueue("If you visit the bird house the next day, there will be some birds.");
                    
                    _todoManager.UpdateTodo("Build a bird house (10 wood, 2 weed). Wait for the next day.");
                    _storyManager.FirstTreeCutDown = true;
                }
                
                manager.Switch<AnimalTamedState>();
            }
        }
    }
}