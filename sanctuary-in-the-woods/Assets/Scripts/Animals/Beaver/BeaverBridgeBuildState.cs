using DefaultNamespace;
using Objects.Animals;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

namespace Animals.Beaver {
    public class BeaverBridgeBuildState : IAnimalState {
        private Tilemap _bottomSolid;
        private Tilemap _bottom;
        private Tilemap _topSolid;
        private Tilemap _top;
        private TileRegistry _tileRegistry;
        private BeaverStorage _beaverStorage;
        
        private static readonly Vector2 BRIDGE_POSITION = new Vector2(-44.0f, -17.0f);

        private void UpdateNavMesh() {
            var navMeshSurface2d = GameObject.FindObjectOfType<NavMeshSurface2d>();
            navMeshSurface2d.BuildNavMeshAsync();
        }
        
        public void OnEnter(AnimalStateManager manager) {
            _bottomSolid = GameObject.Find("BottomSolid").GetComponent<Tilemap>();
            _bottom = GameObject.Find("Bottom").GetComponent<Tilemap>();
            _topSolid = GameObject.Find("TopSolid").GetComponent<Tilemap>();
            _top = GameObject.Find("Top").GetComponent<Tilemap>();
            
            _tileRegistry = GameObject.FindObjectOfType<TileRegistry>();
            _beaverStorage = manager.GetComponent<BeaverStorage>();

            manager.Agent.SetDestination(BRIDGE_POSITION);
        }

        private void ChangeTiles() {
            for (var y = -26; y < -19; y++) {
                for (var x = -54; x < -45; x++) {
                    ChangeTile(new Vector3Int(x, y, 0));
                }
            }
        }
        
        private void ChangeTile(Vector3Int position) {
            var bottomSolidTile = _bottomSolid.GetTile(position);
            if (bottomSolidTile != null) {
                _bottomSolid.SetTile(position, null);
                _bottom.SetTile(position, bottomSolidTile);
            }
        }
        
        public void OnFixedUpdate(AnimalStateManager manager) {
            _tileRegistry = Object.FindObjectOfType<TileRegistry>();
            
            if (manager.Agent.remainingDistance < 0.5f) {
                Object.Instantiate(_beaverStorage.Bridge, new Vector2(-48.6f, -20.0f), Quaternion.identity);
                ChangeTiles();
                UpdateNavMesh();
                manager.Switch<AnimalTamedState>();
            }
        }
    }
}