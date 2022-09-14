using System;
using UnityEngine;
using UnityEngine.AI;

namespace Animals {
    public class AnimalStateManager : MonoBehaviour {
        private AnimalState _currentState;
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;
        
        public float WalkSpeed = 0.5f;
        public float RunSpeed = 1.0f;
        
        public readonly AnimalStateIdle Idle = new AnimalStateIdle();
        public readonly AnimalStateAttracted Attracted = new AnimalStateAttracted();

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            
            Switch(Idle);
        }

        private void Update() {
            _currentState.OnUpdate(this);
        }

        private void FixedUpdate() {
            _currentState.OnFixedUpdate(this);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            _currentState.OnCollisionEnter(this, collision);
        }

        public void Switch(AnimalState state) {
            _currentState = state;
            _currentState.OnEnter(this);
        }
    }
}