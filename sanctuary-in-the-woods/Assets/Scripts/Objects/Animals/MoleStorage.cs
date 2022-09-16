using Animals;
using Animals.Mole;
using Inventory;
using UnityEngine;

namespace Objects.Animals {
    public class MoleStorage : MonoBehaviour {
        private void Start() {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.SeedFeed)));
            stateManager.AddState(new AnimalTamedState(new StackedItem(itemRegistry.SeedFeed, 6), typeof(MolePondBuildState)));
            stateManager.AddState(new MolePondBuildState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}