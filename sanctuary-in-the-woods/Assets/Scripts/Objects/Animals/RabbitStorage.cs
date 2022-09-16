using Animals;
using Animals.Rabbits;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public sealed class RabbitStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.WeedFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.WeedFeed, 2), typeof(RabbitTreeCutState)));
            stateManager.AddState(new RabbitTreeCutState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}