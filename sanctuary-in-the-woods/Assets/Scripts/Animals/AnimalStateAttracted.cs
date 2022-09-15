/*using Objects;
using UnityEngine;

namespace Animals {
    public class AnimalStateAttracted : IAnimalState {
        public const float ATTRACTION_DISTANCE = 15.0f;

        public WeedStorage AttractingStorage;
        
        public void OnEnter(AnimalStateManager manager) {
            manager.Agent.SetDestination(AttractingStorage.transform.position);
        }

        public void OnUpdate(AnimalStateManager manager) {
            
        }

        public void OnFixedUpdate(AnimalStateManager manager) {
            
        }

        public void OnCollisionEnter(AnimalStateManager manager, Collision2D collision) {
            if (collision.gameObject.TryGetComponent<WeedStorage>(out var weedStorage)) {
                manager.Agent.isStopped = true;
                manager.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                
                weedStorage.Clear();

                manager.Agent.isStopped = false;
                manager.Switch(manager.GetState<AnimalIdleState>());
            }
        }
    }
}*/