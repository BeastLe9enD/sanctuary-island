using Animals;
using Animals.Bears;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public sealed class BearStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.BerryFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.BerryFeed), typeof(BearDestroyRockState)));
            stateManager.AddState(new BearDestroyRockState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}