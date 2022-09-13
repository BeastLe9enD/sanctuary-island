using System;
using UnityEngine;
using URandom = Unity.Mathematics.Random;

namespace Inventory.Animal {
    public class AnimalStateIdle : AnimalState {
        private const float WAIT_TIME = 1.0f;
        
        private Rigidbody2D _rigidbody;

        private Vector2 _origin;
        private float _radius;

        private Vector2 _requestedPosition;
        private Vector2 _direction;
        private float _requestedLength;
        private float _traveledLength;

        private float _requestedWaitTime;
        private float _waitedTime;
        private void GenerateNewTask(Vector2 srcPosition) {
            var random = new URandom();
            random.InitState((uint)DateTime.Now.Ticks);
            
            var angle = random.NextFloat(Mathf.PI * 2.0f);
            var length = random.NextFloat(_radius);
            
            _requestedPosition = _origin + new Vector2(Mathf.Cos(angle) * length, Mathf.Sin(angle) * length);
            _requestedLength = (_requestedPosition - srcPosition).magnitude;
            _traveledLength = 0.0f;

            _direction = (_requestedPosition - srcPosition).normalized;

            _waitedTime = 0.0f;
            _requestedWaitTime = random.NextFloat(2.0f, 6.0f);
        }    
            
        public override void OnEnter(AnimalStateManager manager) {
            _rigidbody = manager.GetComponent<Rigidbody2D>();

            _origin = manager.transform.position;
            _radius = 5.0f;

            GenerateNewTask(_origin);
        }

        public override void OnUpdate(AnimalStateManager manager) {
            
        }

        public override void OnFixedUpdate(AnimalStateManager manager) {
            Vector2 playerPosition = manager.transform.position;
            
            if (_traveledLength < _requestedLength) {
                _rigidbody.velocity = _direction * manager.WalkSpeed;
                _traveledLength += manager.WalkSpeed / 50.0f;
                return;
            }

            _rigidbody.velocity = Vector2.zero;

            if (_waitedTime < _requestedWaitTime) {
                _waitedTime += Time.deltaTime;
                return;
            }
            
            GenerateNewTask(playerPosition);
        }

        public override void OnCollisionEnter(AnimalStateManager manager) {
            
        }
    }
}