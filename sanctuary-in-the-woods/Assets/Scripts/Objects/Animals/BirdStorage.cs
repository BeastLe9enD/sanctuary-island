using Animals;
using Animals.Birds;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public sealed class BirdStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.WeedFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.WeedFeed), typeof(BirdTradeState)));
            stateManager.AddState(new BirdTradeState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}