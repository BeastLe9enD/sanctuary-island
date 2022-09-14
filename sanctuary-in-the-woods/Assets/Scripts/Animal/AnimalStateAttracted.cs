using UnityEngine;

namespace Inventory.Animal {
    public class AnimalStateAttracted : AnimalState {
        public const float ATTRACTION_DISTANCE = 15.0f;

        public WeedStorage AttractingStorage;
        
        public override void OnEnter(AnimalStateManager manager) {
            Debug.Log($"Attracted to storage on place {manager.transform}");
        }

        public override void OnUpdate(AnimalStateManager manager) {
            
        }

        public override void OnFixedUpdate(AnimalStateManager manager) {
            
        }

        public override void OnCollisionEnter(AnimalStateManager manager) {
            
        }
    }
}