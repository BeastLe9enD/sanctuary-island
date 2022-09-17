using Animals;
using Animals.Dragons;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public class DragonStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.BerryFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.Cake), typeof(DragonDestroyIceState)));
            stateManager.AddState(new DragonDestroyIceState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}