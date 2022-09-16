using System;
using Inventory;
using Story;
using UI;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;
using URandom = Unity.Mathematics.Random;

namespace Animals {
    public sealed class AnimalIdleState : IAnimalState {
        private const float WAIT_TIME = 1.0f;

        private PlayerInventory _playerInventory;
        private ParticleSystem _particleSystem;
        private StoryManager _storyManager;
        private PopupManager _popupManager;

        private StackedItem _tameStack;

        private Vector2 _origin;
        private float _radius;

        private bool _targetFound;

        private Vector2 _requestedPosition;
        private float _waitedTime;
        private float _timeToWait;

        private float _moveStartTime;

        private bool _isTamed;
        private float _tameStart;

        public AnimalIdleState(StackedItem tameStack)
        {
            _tameStack = tameStack;
        }
        
        private bool GenerateNewTask(Vector2 srcPosition, AnimalStateManager manager) {
            var random = new URandom();
            random.InitState((uint)DateTime.Now.Ticks ^ (uint)srcPosition.GetHashCode());
            
            var angle = random.NextFloat(Mathf.PI * 2.0f);
            var length = random.NextFloat(1.0f, _radius);
            
            _requestedPosition = _origin + new Vector2(Mathf.Cos(angle) * length, Mathf.Sin(angle) * length);

            _waitedTime = 0.0f;
            _timeToWait = random.NextFloat(2.0f, 6.0f);
            _moveStartTime = Time.time;
            
            manager.Agent.SetDestination(_requestedPosition);

            if (!NavMeshUtils.IsAccessible(_requestedPosition))
            {
                return false;
            }

            return true;
        }

        public void OnEnter(AnimalStateManager manager)
        {
            _isTamed = false;
            _tameStart = 0.0f;
            
            _origin = manager.transform.position;
            _radius = 5.0f;

            _playerInventory = Object.FindObjectOfType<PlayerInventory>();
            _particleSystem = manager.GetComponent<ParticleSystem>();
            _storyManager = Object.FindObjectOfType<StoryManager>();
            _popupManager = Object.FindObjectOfType<PopupManager>();
            _targetFound = GenerateNewTask(_origin, manager);
        }

        public void OnFixedUpdate(AnimalStateManager manager) {
            if (_isTamed)
            {
                if (Time.time - _tameStart >= 3.0)
                {
                    if (!_storyManager.FirstAnimalTamed)
                    {
                        _popupManager.Enqueue("After the animal has been tamed, it remains sitting on the ground.");
                        _popupManager.Enqueue("You can right-click the animal to make it follow you.");
                        _popupManager.Enqueue("If you press the right mouse button again, the animal will stay seated again.");
                        _popupManager.Enqueue("If you left click the rabbit with 2 weed feed, it will cut down a tree.");
                        _storyManager.FirstAnimalTamed = true;
                    }
                    
                    _particleSystem.Stop();
                    manager.Switch<AnimalTamedState>();
                }

                return;
            }

            if (!_targetFound)
            {
                _origin = manager.transform.position;
                _radius = 5.0f;
                _targetFound = GenerateNewTask(_origin, manager);
            }
            
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

        public void OnMouseOver(AnimalStateManager manager)
        {
            if (!Input.GetMouseButtonDown(0) || _isTamed) return;

            if (!_playerInventory.CanRemove(_tameStack))
            {
                return;
            }
            
            _playerInventory.Remove(_tameStack);

            _isTamed = true;
            _tameStart = Time.time;
            _particleSystem.Play();
        }
    }
}