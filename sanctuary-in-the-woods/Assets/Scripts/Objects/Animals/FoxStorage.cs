using Animals;
using Animals.Foxes;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public class FoxStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.WeedFeed), typeof(FoxFollowState)));
            stateManager.AddState(new FoxFollowState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}