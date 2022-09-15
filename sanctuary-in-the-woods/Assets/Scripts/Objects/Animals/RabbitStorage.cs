using Animals;
using Animals.Rabbits;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public class RabbitStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.WeedFeed)));
            stateManager.AddState(new AnimalTamedState(typeof(RabbitTreeCutState)));
            stateManager.AddState(new RabbitTreeCutState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}