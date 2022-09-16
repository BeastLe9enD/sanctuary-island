using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

namespace Animals.Mole {
    public class MolePondBuildState : IAnimalState {
        private Tilemap _tilemap;
        private static readonly Vector2 POND_POSITION = new Vector2(-6.17f, -2.52f);

        private void PlacePond() {
            var firstTile = _tilemap.WorldToCell(POND_POSITION);
        }
        public void OnEnter(AnimalStateManager manager) {
            //_tilemap = GameObject.Find("BottomSolid").GetComponent<Tilemap>();
            
            manager.Agent.SetDestination(POND_POSITION);
        }

        public void OnFixedUpdate(AnimalStateManager manager) {
            if (manager.Agent.remainingDistance < 0.5f) {
                PlacePond();
                manager.Switch<AnimalTamedState>();
            }
        }
    }
}