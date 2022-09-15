using System;
using Inventory;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using URandom = Unity.Mathematics.Random;

namespace Animals {
    public class AnimalIdleState : IAnimalState {
        private const float WAIT_TIME = 1.0f;

        private PlayerInventory _playerInventory;

        private StackedItem _tameStack;
        private Type _tamedStateType;

        private Vector2 _origin;
        private float _radius;

        private Vector2 _requestedPosition;
        private float _waitedTime;
        private float _timeToWait;

        private float _moveStartTime;

        public AnimalIdleState(StackedItem tameStack, Type tamedStateType)
        {
            _tameStack = tameStack;
            _tamedStateType = tamedStateType;
        }
        
        private void GenerateNewTask(Vector2 srcPosition, AnimalStateManager manager) {
            var random = new URandom();
            random.InitState((uint)DateTime.Now.Ticks);
            
            var angle = random.NextFloat(Mathf.PI * 2.0f);
            var length = random.NextFloat(1.0f, _radius);
            
            _requestedPosition = _origin + new Vector2(Mathf.Cos(angle) * length, Mathf.Sin(angle) * length);

            _waitedTime = 0.0f;
            _timeToWait = random.NextFloat(2.0f, 6.0f);
            _moveStartTime = Time.time;
            
            manager.Agent.SetDestination(_requestedPosition);

            if (!NavMeshUtils.IsAccessible(_requestedPosition)) {
                GenerateNewTask(srcPosition, manager);
            }
        }

        public void OnEnter(AnimalStateManager manager) {
            _origin = manager.transform.position;
            _radius = 5.0f;

            _playerInventory = Object.FindObjectOfType<PlayerInventory>();

            GenerateNewTask(_origin, manager);
        }

        public void OnUpdate(AnimalStateManager manager) {
            
        }

        public void OnFixedUpdate(AnimalStateManager manager) {
            Vector2 playerPosition = manager.transform.position;

            var agent = manager.Agent;

            var shouldWait = true;
            if (agent.remainingDistance >= agent.stoppingDistance)
            {
                if (Time.time - _moveStartTime <= 4.0)
                {
                    return;
                }

                shouldWait = false;
            }

            if (!shouldWait)
            {
                _waitedTime = _timeToWait;
            }
            
            if (_waitedTime < _timeToWait) {
                _waitedTime += Time.deltaTime;
                return;
            }

            GenerateNewTask(playerPosition, manager);
        }

        public void OnCollisionEnter(AnimalStateManager manager, Collision2D collision) { }

        public void OnMouseOver(AnimalStateManager manager)
        {
            if (!Input.GetMouseButtonDown(0)) return;

            if (!_playerInventory.CanRemove(_tameStack))
            {
                return;
            }
            
            _playerInventory.Remove(_tameStack);
            
            Debug.Log("TAMED");

            manager.Switch(manager.GetState(_tamedStateType));
        }
    }
}