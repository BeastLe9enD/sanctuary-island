using System;
using UnityEngine;
using UnityEngine.AI;

namespace Animals {
    public class AnimalStateManager : MonoBehaviour {
        private AnimalState _currentState;
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;
        private Animator _animator;

        private bool _lookRight;
        private GameObject _gameObject;

        public readonly AnimalStateIdle Idle = new AnimalStateIdle();
        public readonly AnimalStateAttracted Attracted = new AnimalStateAttracted();

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _animator = GetComponent<Animator>();
            _gameObject = gameObject;
            Switch(Idle);
        }

        private void Update() {
            _currentState.OnUpdate(this);
            Debug.Log( gameObject.name + ": " + _agent.velocity);
        }

        private void FixedUpdate() {
            _animator.SetBool("isWalking", _agent.velocity != Vector3.zero);
            if (_agent.velocity.x > 0 && !_lookRight) {
                Flip();
            }
            else if (_agent.velocity.x < 0 && _lookRight) {
                Flip();
            }
            _currentState.OnFixedUpdate(this);
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            _currentState.OnCollisionEnter(this, collision);
        }

        public void Switch(AnimalState state) {
            _currentState = state;
            _currentState.OnEnter(this);
        }

        private void Flip() {
            Debug.Log("flip");
            var currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            _gameObject.transform.localScale = currentScale;
            _lookRight = !_lookRight;
        }
    }
}