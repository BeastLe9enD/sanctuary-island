using System;
using UnityEngine;
using UnityEngine.AI;

namespace Animals {
    public class AnimalStateManager : MonoBehaviour {
        private AnimalState _currentState;
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;
        
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private bool _lookRight;
        
        private GameObject _gameObject;

        public readonly AnimalStateIdle Idle = new AnimalStateIdle();
        public readonly AnimalStateAttracted Attracted = new AnimalStateAttracted();

        private const float _THRESHOLD = 0.1f;
        
        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _animator = GetComponent<Animator>();
            _gameObject = gameObject;
            Switch(Idle);
        }

        private void Update() {
            _animator.SetBool(IsWalking, _agent.velocity != Vector3.zero);
            if (_agent.velocity.x > _THRESHOLD && !_lookRight) {
                Flip();
            }
            else if (_agent.velocity.x < -_THRESHOLD && _lookRight) {
                Flip();
            }
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

        private void Flip() {
            var currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            _gameObject.transform.localScale = currentScale;
            _lookRight = !_lookRight;
        }
    }
}