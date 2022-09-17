using Story;
using UnityEngine;

namespace Animals.Flamingos
{
    public sealed class FlamingoGreenOasisState : IAnimalState
    {
        private static readonly Vector2 OASIS_POSITION = new Vector2(38.7704f, -112.9064f);

        private StoryManager _storyManager;
        
        public void OnEnter(AnimalStateManager manager)
        {
            _storyManager = Object.FindObjectOfType<StoryManager>();

            manager.Agent.SetDestination(OASIS_POSITION);
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            if (manager.Agent.remainingDistance <= 1.0)
            {
                var palmTree = _storyManager.PalmTree;
                
                Object.Instantiate(palmTree, new Vector3(28.01f, -107.73f, 1.0f), Quaternion.identity);
                Object.Instantiate(palmTree, new Vector3(30.28f, -98.67f, 1.0f), Quaternion.identity);
                Object.Instantiate(palmTree, new Vector3(37.64f, -94.31f, 1.0f), Quaternion.identity);
                Object.Instantiate(palmTree, new Vector3(48.98f, -98.56f, 1.0f), Quaternion.identity);
            }
        }
    }
}