using Animals;
using Animals.Elephants;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public sealed class ElephantStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.BerryFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.BerryFeed), typeof(ElephantDestroyRockState)));
            stateManager.AddState(new ElephantDestroyRockState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}