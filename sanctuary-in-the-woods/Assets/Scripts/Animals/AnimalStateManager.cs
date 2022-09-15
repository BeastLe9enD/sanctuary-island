using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Animals {
    public class AnimalStateManager : MonoBehaviour {
        private const float _THRESHOLD = 0.3f;
        
        private IAnimalState _currentState;
        private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;
        
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private bool _lookRight;
        
        private GameObject _gameObject;

        private readonly Dictionary<Type, IAnimalState> _states = new();

        private void Start() {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
            _animator = GetComponent<Animator>();
            _gameObject = gameObject;
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

        private void OnMouseOver()
        {
            _currentState.OnMouseOver(this);
        }

        public void Switch(IAnimalState state) {
            _currentState = state;
            _currentState.OnEnter(this);
        }

        public void Switch<T>() where T : class, IAnimalState
        {
            Switch(GetState<T>());
        }

        private void Flip() {
            var currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            _gameObject.transform.localScale = currentScale;
            _lookRight = !_lookRight;
        }
        
        #region STATES

        public IAnimalState GetState(Type type)
        {
            if (!_states.TryGetValue(type, out var result))
            {
                throw new Exception($"State is not present: {type.FullName}");
            }

            return result;
        }
        
        public T GetState<T>() where T : class, IAnimalState
        {
            if (!_states.TryGetValue(typeof(T), out var result))
            {
                throw new Exception($"State is not present: {typeof(T).FullName}");
            }

            return (T)result;
        }

        public void AddState(IAnimalState state)
        {
            _states.Add(state.GetType(), state);
        }
        
        #endregion
    }
}