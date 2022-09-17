using System.Linq;
using Objects;
using UnityEngine;
using Utils;

namespace Animals.Dragons
{
    public sealed class DragonDestroyIceState : IAnimalState
    {
        private IcebergStorage _icebergStorage;
        
        public void OnEnter(AnimalStateManager manager)
        {
            var objects = Object.FindObjectsOfType<IcebergStorage>();
            if (objects.Length == 0)
            {
                manager.Switch<AnimalTamedState>();
                return;
            }
            
            _icebergStorage = Object.FindObjectsOfType<IcebergStorage>()
                .OrderBy(tree => Vector3.Distance(tree.transform.position, manager.transform.position))
                .First();

            manager.Agent.SetDestination(_icebergStorage.transform.position);
        }

        public void OnMouseOver(AnimalStateManager manager)
        {
            Debug.Log(manager.Agent.remainingDistance);
            if (manager.Agent.remainingDistance <= 50.0)
            {
                Object.Destroy(_icebergStorage.gameObject);
                NavMeshUtils.UpdateMesh();
                
                manager.Switch<AnimalTamedState>();
            }
        }
    }
}