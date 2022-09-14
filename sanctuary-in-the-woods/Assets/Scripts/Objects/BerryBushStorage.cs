using System;
using Inventory;
using UnityEngine;
using URandom = Unity.Mathematics.Random;

namespace Objects
{
    public class BerryBushStorage : MonoBehaviour
    {
        public Sprite EmptySprite;
        public Sprite GrowingSprite;
        public Sprite MatureSprite;

        private PlayerInventory _playerInventory;
        private ItemRegistry _itemRegistry;
        
        private State _state;
        private int _elapsedTime;
        
        public enum State
        {
            Empty,
            Growing,
            Mature
        }

        void Start()
        {
            _playerInventory = FindObjectOfType<PlayerInventory>();
            _itemRegistry = FindObjectOfType<ItemRegistry>();
        }

        private Sprite GetSprite(State state)
        {
            switch (state)
            {
                case State.Empty:
                    return EmptySprite; 
                case State.Growing:
                    return GrowingSprite;
                case State.Mature:
                    return MatureSprite;
                default:
                    return null;
            }
        }
        
        private void UpdateState(State state)
        {
            _state = state;
            GetComponent<SpriteRenderer>().sprite = GetSprite(state);
        }
        
        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(1)) return;
            if (_state != State.Mature) return;

            var random = new URandom((uint)DateTime.Now.Ticks);
            var stack = new StackedItem(_itemRegistry.Berries, 1 + random.NextInt(2));

            if (_playerInventory.CanAdd(stack))
            {
                _playerInventory.Add(stack);
                UpdateState(State.Empty);
            }
        }

        void FixedUpdate()
        {
            if (_state != State.Mature && ++_elapsedTime > 200)
            {
                _elapsedTime = 0;
                UpdateState(_state == State.Empty ? State.Growing : State.Mature);
            }
        }
    }
}