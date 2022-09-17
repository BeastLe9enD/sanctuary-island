using Animals;
using Animals.Kangaroos;
using Inventory;
using UnityEngine;

namespace Objects.Animals
{
    public class KangarooStorage : MonoBehaviour
    {
        void Start()
        {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.WeedFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.WeedFeed), typeof(KangarooTradeState)));
            stateManager.AddState(new KangarooTradeState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}