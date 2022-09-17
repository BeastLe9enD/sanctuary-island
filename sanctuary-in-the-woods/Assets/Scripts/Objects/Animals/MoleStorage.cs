using Animals;
using Animals.Mole;
using Inventory;
using UnityEngine;

namespace Objects.Animals {
    public sealed class MoleStorage : MonoBehaviour {
        private void Start() {
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var stateManager = GetComponent<AnimalStateManager>();
            stateManager.AddState(new AnimalIdleState(new StackedItem(itemRegistry.SeedFeed), typeof(MoleTamedState)));
            stateManager.AddState(new MoleTamedState());
            stateManager.AddState(new MolePondBuildState());
            
            stateManager.Switch<AnimalIdleState>();
        }
    }
}