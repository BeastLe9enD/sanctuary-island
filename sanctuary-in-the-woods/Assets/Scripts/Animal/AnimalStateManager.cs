using UnityEngine;

namespace Inventory.Animal {
    public class AnimalStateManager : MonoBehaviour {
        private AnimalState _currentState;

        public float WalkSpeed = 0.5f;
        public float RunSpeed = 1.0f;
        
        public readonly AnimalStateIdle Idle = new AnimalStateIdle();
        public readonly AnimalStateAttracted Attracted = new AnimalStateAttracted();

        private void Start() {
            Switch(Idle);
        }

        private void Update() {
            _currentState.OnUpdate(this);
        }

        private void FixedUpdate() {
            _currentState.OnFixedUpdate(this);
        }

        public void Switch(AnimalState state) {
            _currentState = state;
            _currentState.OnEnter(this);
        }
    }
}