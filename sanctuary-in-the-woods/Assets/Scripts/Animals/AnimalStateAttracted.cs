using Objects;
using UnityEngine;

namespace Animals {
    public class AnimalStateAttracted : AnimalState {
        public const float ATTRACTION_DISTANCE = 15.0f;

        public WeedStorage AttractingStorage;
        
        public override void OnEnter(AnimalStateManager manager) {
            manager.Agent.SetDestination(AttractingStorage.transform.position);
        }

        public override void OnUpdate(AnimalStateManager manager) {
            
        }

        public override void OnFixedUpdate(AnimalStateManager manager) {
            
        }

        public override void OnCollisionEnter(AnimalStateManager manager, Collision2D collision) {
            if (collision.gameObject.TryGetComponent<WeedStorage>(out var weedStorage)) {
                manager.Agent.isStopped = true;
                manager.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                
                weedStorage.Clear();

                manager.Agent.isStopped = false;
                manager.Switch(manager.Idle);
            }
        }
    }
}