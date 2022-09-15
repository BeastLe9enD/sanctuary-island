using System;
using Objects;
using UnityEngine;
using Utils;
using URandom = Unity.Mathematics.Random;

namespace Animals {
    public class AnimalStateIdle : AnimalState {
        private const float WAIT_TIME = 1.0f;
        
        private Rigidbody2D _rigidbody;

        private Vector2 _origin;
        private float _radius;

        private Vector2 _requestedPosition;
        private float _waitedTime;
        private float _timeToWait;

        private float _moveStartTime;


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
                return;
            }
            
            Debug.Log($"Setting destination to {_requestedPosition}");
        }

        private void CheckForAttraction(AnimalStateManager manager) {
            var selectedIndex = -1;
            var lastDistance = float.PositiveInfinity;
            
            var weedStorages = GameObject.FindObjectsOfType<WeedStorage>();

            for (var i = 0; i < weedStorages.Length; i++) {
                var weedStorage = weedStorages[i];
                if (!weedStorage.CanAttactAnimals()) continue;
                
                Vector2 weedStoragePosition = weedStorage.gameObject.transform.position;
                var distance = Mathf.Abs(Vector2.Distance(weedStoragePosition, manager.transform.position));

                if (lastDistance < distance) continue;
                lastDistance = distance;
                selectedIndex = i;
            }

            if (lastDistance > AnimalStateAttracted.ATTRACTION_DISTANCE || selectedIndex == -1) return;

            manager.Attracted.AttractingStorage = weedStorages[selectedIndex];
            manager.Switch(manager.Attracted);
        }
            
        public override void OnEnter(AnimalStateManager manager) {
            _rigidbody = manager.GetComponent<Rigidbody2D>();

            _origin = manager.transform.position;
            _radius = 5.0f;

            GenerateNewTask(_origin, manager);
        }

        public override void OnUpdate(AnimalStateManager manager) {
            
        }

        public override void OnFixedUpdate(AnimalStateManager manager) {
            CheckForAttraction(manager);
            
            Vector2 playerPosition = manager.transform.position;

            var agent = manager.Agent;
            Debug.Log(agent.remainingDistance + ":" + (Time.time - _moveStartTime));

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

        public override void OnCollisionEnter(AnimalStateManager manager, Collision2D collision) {
            
        }
    }
}