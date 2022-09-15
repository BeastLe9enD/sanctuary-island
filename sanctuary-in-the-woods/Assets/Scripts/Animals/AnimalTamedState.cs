﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Animals
{
    public sealed class AnimalTamedState : IAnimalState
    {
        private Player _player;

        private Type _actionType;
        
        private bool _shouldFollow;
        private float _lastUpdateTime;

        public AnimalTamedState(Type actionType)
        {
            _actionType = actionType;
        }
        
        public void OnEnter(AnimalStateManager manager)
        {
            _shouldFollow = false;
            _player = Object.FindObjectOfType<Player>();
        }

        public void OnUpdate(AnimalStateManager manager)
        {
            if (!_shouldFollow)
            {
                return;
            }

            if (Time.time - _lastUpdateTime >= 0.5)
            {
                _lastUpdateTime = Time.time;
                manager.Agent.SetDestination(_player.transform.position);
            }
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            
        }

        public void OnCollisionEnter(AnimalStateManager manager, Collision2D collision)
        {
            
        }

        public void OnMouseOver(AnimalStateManager manager)
        {
            if (Input.GetMouseButtonDown(0))
            {
                manager.Switch(manager.GetState(_actionType));
                return;
            }
            if (Input.GetMouseButtonDown(1))
            {
                _shouldFollow = !_shouldFollow;
            }
        }
    }
}