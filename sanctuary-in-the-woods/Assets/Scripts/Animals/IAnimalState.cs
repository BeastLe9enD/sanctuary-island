using UnityEngine;

namespace Animals {
    public interface IAnimalState {
        void OnEnter(AnimalStateManager manager);
        void OnUpdate(AnimalStateManager manager);
        void OnFixedUpdate(AnimalStateManager manager);

        void OnCollisionEnter(AnimalStateManager manager, Collision2D collision);

        void OnMouseOver(AnimalStateManager manager);
    }
}