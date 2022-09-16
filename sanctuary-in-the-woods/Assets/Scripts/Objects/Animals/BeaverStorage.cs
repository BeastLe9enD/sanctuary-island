using Animals;
using Animals.Beaver;
using Inventory;
using UnityEngine;

namespace Objects.Animals {
    public class BeaverStorage : MonoBehaviour {
        public GameObject Bridge;
        private void Start() {
            var itemRegistry = FindObjectOfType<ItemRegistry>();
            
            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.BerryFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.BerryFeed, 2), typeof(BeaverBridgeBuildState)));
            stateManager.AddState(new BeaverBridgeBuildState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}