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
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.WeedFeed), 
                typeof(RabbitTamedState)));
            stateManager.AddState(new RabbitTamedState());
            stateManager.AddState(new RabbitTreeCutState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}