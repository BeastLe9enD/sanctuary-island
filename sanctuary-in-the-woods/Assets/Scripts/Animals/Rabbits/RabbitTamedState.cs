using UnityEngine;

namespace Animals.Rabbits
{
    public class RabbitTamedState : IAnimalState
    {
        public void OnEnter(AnimalStateManager manager) { }

        public void OnUpdate(AnimalStateManager manager) { }

        public void OnFixedUpdate(AnimalStateManager manager) { }

        public void OnCollisionEnter(AnimalStateManager manager, Collision2D collision)
        {
            
        }

        public void OnMouseOver(AnimalStateManager manager)
        {
            
        }
    }
}