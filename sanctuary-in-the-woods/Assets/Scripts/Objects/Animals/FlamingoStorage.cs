using Animals;
using Animals.Flamingos;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public class FlamingoStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.BerryFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.BerryFeed, 4), typeof(FlamingoGreenOasisState)));
            stateManager.AddState(new FlamingoGreenOasisState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}