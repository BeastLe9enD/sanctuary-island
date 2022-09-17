using UnityEngine;

namespace Animals.Foxes
{
    public sealed class FoxFollowState : IAnimalState
    {
        private Player _player;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _player = Object.FindObjectOfType<Player>();
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            manager.Agent.SetDestination(_player.transform.position);
        }
    }
}