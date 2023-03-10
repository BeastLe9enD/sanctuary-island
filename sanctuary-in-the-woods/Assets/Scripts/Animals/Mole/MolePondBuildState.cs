using DefaultNamespace;
using Story;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Animals.Mole {
    public class MolePondBuildState : IAnimalState {
        private Tilemap _bottomSolid;
        private Tilemap _bottom;
        private Tilemap _topSolid;
        private Tilemap _top;
        private TileRegistry _tileRegistry;
        private StoryManager _storyManager;
        
        public static readonly Vector2 POND_POSITION = new Vector2(-6.17f, -4.52f);
        public static readonly Vector2 SECOND_POND_POSITION = new Vector2(-117.382f, -8.816619f);

        private void ChangeTile(Vector3Int position, TileBase tile) {
            _top.SetTile(position, null);
            _topSolid.SetTile(position, null);
            
            _bottom.SetTile(position, tile);
        }

        private void PlacePond(Vector2 worldPondPos) {
            var firstTile = _bottomSolid.WorldToCell(worldPondPos);
            firstTile = new Vector3Int(firstTile.x + 4, firstTile.y - 4, firstTile.z);

            for (var y = 0; y < 9; y++) {
                for (var x = 0; x < 11; x++) {
                    ChangeTile(new Vector3Int(firstTile.x + x, firstTile.y + y, firstTile.z), _tileRegistry.Tile108);
                }
            }

            for (var x = 3; x < 8; x++) {
                ChangeTile(new Vector3Int(firstTile.x + x, firstTile.y -1, firstTile.z), _tileRegistry.Tile108);
                ChangeTile(new Vector3Int(firstTile.x + x, firstTile.y -2, firstTile.z), _tileRegistry.Tile102);
            }

            for (var x = 5; x < 7; x++) {
                ChangeTile(new Vector3Int(firstTile.x + x, firstTile.y -2 , firstTile.z), _tileRegistry.Tile108);
                ChangeTile(new Vector3Int(firstTile.x + x, firstTile.y - 3, firstTile.z), _tileRegistry.Tile102);
            }

            for (var y = 1; y < 7; y++) {
                ChangeTile(new Vector3Int(firstTile.x - 1, firstTile.y + y, firstTile.z), _tileRegistry.Tile108);
                ChangeTile(new Vector3Int(firstTile.x - 2, firstTile.y + y, firstTile.z), _tileRegistry.Tile93);
            }

            for (var x = 1; x < 9; x++) {
                ChangeTile(new Vector3Int(firstTile.x + x, firstTile.y + 9, firstTile.z), _tileRegistry.Tile108);
                ChangeTile(new Vector3Int(firstTile.x + x, firstTile.y + 10, firstTile.z), _tileRegistry.Tile37);
            }

            for (var y = 2; y < 7; y++) {
                ChangeTile(new Vector3Int(firstTile.x + 11, firstTile.y + y, firstTile.z), _tileRegistry.Tile108);
                ChangeTile(new Vector3Int(firstTile.x + 12, firstTile.y + y, firstTile.z), _tileRegistry.Tile47);
            }
            
            //
            ChangeTile(new Vector3Int(firstTile.x - 2, firstTile.y + 7, firstTile.z), _tileRegistry.Tile36);
            ChangeTile(new Vector3Int(firstTile.x - 1, firstTile.y + 9, firstTile.z), _tileRegistry.Tile36);
            ChangeTile(new Vector3Int(firstTile.x, firstTile.y + 10, firstTile.z), _tileRegistry.Tile36);
            ChangeTile(new Vector3Int(firstTile.x - 1, firstTile.y + 8, firstTile.z), _tileRegistry.Tile93);
            ChangeTile(new Vector3Int(firstTile.x - 1, firstTile.y + 7, firstTile.z), _tileRegistry.Tile146);
            ChangeTile(new Vector3Int(firstTile.x, firstTile.y + 9, firstTile.z), _tileRegistry.Tile146);
            
            ChangeTile(new Vector3Int(firstTile.x - 2, firstTile.y, firstTile.z), _tileRegistry.Tile195);
            ChangeTile(new Vector3Int(firstTile.x - 1, firstTile.y - 1, firstTile.z), _tileRegistry.Tile195);
            ChangeTile(new Vector3Int(firstTile.x - 1, firstTile.y, firstTile.z), _tileRegistry.Tile140);
            ChangeTile(new Vector3Int(firstTile.x + 2, firstTile.y - 2, firstTile.z), _tileRegistry.Tile195);
            ChangeTile(new Vector3Int(firstTile.x, firstTile.y - 1, firstTile.z), _tileRegistry.Tile102);
            ChangeTile(new Vector3Int(firstTile.x + 1, firstTile.y - 1, firstTile.z), _tileRegistry.Tile102);
            ChangeTile(new Vector3Int(firstTile.x + 2, firstTile.y - 1, firstTile.z), _tileRegistry.Tile140);
            ChangeTile(new Vector3Int(firstTile.x + 4, firstTile.y - 2, firstTile.z), _tileRegistry.Tile140);
            ChangeTile(new Vector3Int(firstTile.x + 4, firstTile.y - 3, firstTile.z), _tileRegistry.Tile195);
            ChangeTile(new Vector3Int(firstTile.x + 7, firstTile.y - 2, firstTile.z), _tileRegistry.Tile139);
            ChangeTile(new Vector3Int(firstTile.x + 7, firstTile.y - 3, firstTile.z), _tileRegistry.Tile52);
            ChangeTile(new Vector3Int(firstTile.x + 8, firstTile.y - 2, firstTile.z), _tileRegistry.Tile102);
            ChangeTile(new Vector3Int(firstTile.x + 9, firstTile.y - 1, firstTile.z), _tileRegistry.Tile139);
            ChangeTile(new Vector3Int(firstTile.x + 9, firstTile.y - 2, firstTile.z), _tileRegistry.Tile52);
            ChangeTile(new Vector3Int(firstTile.x + 8, firstTile.y - 1, firstTile.z), _tileRegistry.Tile108);
            ChangeTile(new Vector3Int(firstTile.x + 10, firstTile.y - 1, firstTile.z), _tileRegistry.Tile102);
            ChangeTile(new Vector3Int(firstTile.x + 11, firstTile.y - 1, firstTile.z), _tileRegistry.Tile52);
            ChangeTile(new Vector3Int(firstTile.x + 11, firstTile.y, firstTile.z), _tileRegistry.Tile47);
            ChangeTile(new Vector3Int(firstTile.x + 11, firstTile.y + 1, firstTile.z), _tileRegistry.Tile139);
            ChangeTile(new Vector3Int(firstTile.x + 12, firstTile.y + 1, firstTile.z), _tileRegistry.Tile52);
            ChangeTile(new Vector3Int(firstTile.x + 12, firstTile.y + 7, firstTile.z), _tileRegistry.Tile38);
            ChangeTile(new Vector3Int(firstTile.x + 11, firstTile.y + 8, firstTile.z), _tileRegistry.Tile38);
            ChangeTile(new Vector3Int(firstTile.x + 11, firstTile.y + 7, firstTile.z), _tileRegistry.Tile145);
            ChangeTile(new Vector3Int(firstTile.x + 10, firstTile.y + 9, firstTile.z), _tileRegistry.Tile38);
            ChangeTile(new Vector3Int(firstTile.x + 9, firstTile.y + 10, firstTile.z), _tileRegistry.Tile38);
            ChangeTile(new Vector3Int(firstTile.x + 9, firstTile.y + 9, firstTile.z), _tileRegistry.Tile145);
            ChangeTile(new Vector3Int(firstTile.x + 10, firstTile.y + 8, firstTile.z), _tileRegistry.Tile145);
        }
        public void OnEnter(AnimalStateManager manager) {
            _tileRegistry = Object.FindObjectOfType<TileRegistry>();
            _storyManager = Object.FindObjectOfType<StoryManager>();

            if (_storyManager.PondPlaced && _storyManager.SecondPondPlaced)
            {
                manager.Switch<AnimalTamedState>();
            }
            
            _bottomSolid = GameObject.Find("BottomSolid").GetComponent<Tilemap>();
            _bottom = GameObject.Find("Bottom").GetComponent<Tilemap>();
            _topSolid = GameObject.Find("TopSolid").GetComponent<Tilemap>();
            _top = GameObject.Find("Top").GetComponent<Tilemap>();

            if (_storyManager.PondPlaced)
            {
                manager.Agent.SetDestination(SECOND_POND_POSITION);
            }
            else
            {
                manager.Agent.SetDestination(POND_POSITION);
            }
        }

        public void OnFixedUpdate(AnimalStateManager manager) {
            if (manager.Agent.remainingDistance < 0.5f)
            {
                var pondPosition = _storyManager.PondPlaced ? SECOND_POND_POSITION : POND_POSITION;
                
                manager.Agent.SetDestination(new Vector2(pondPosition.x - 3.0f, pondPosition.y));

                if (!_storyManager.PondPlaced)
                {
                    PlacePond(POND_POSITION);
                    _storyManager.PondPlaced = true;
                }
                else
                {
                    PlacePond(SECOND_POND_POSITION);
                    _storyManager.SecondPondPlaced = true;
                }
                
                manager.Switch<MoleTamedState>();
            }
        }
    }
}