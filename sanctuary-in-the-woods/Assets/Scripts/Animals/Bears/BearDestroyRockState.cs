using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Animals.Bears
{
    public class BearDestroyRockState : IAnimalState
    {
        private TileRegistry _tileRegistry;

        private Vector3Int _cellPosition;
        private Vector3 _position;

        private Vector3Int? FindTargetRock(Tilemap tilemap)
        {
            var availableTiles = new List<Vector3Int>();

            for (var j = tilemap.cellBounds.yMin; j < tilemap.cellBounds.yMax; j++)
            {
                for (var i = tilemap.cellBounds.xMin; i < tilemap.cellBounds.xMax; i++)
                {
                    var position = new Vector3Int(i, j, 0);
                    var tile = tilemap.GetTile(position);

                    if (tile == null)
                    {
                        continue;
                    }

                    availableTiles.Add(position);
                }
            }

            if (availableTiles.Count == 0)
            {
                return null;
            }

            return availableTiles.OrderBy(x => tilemap.CellToWorld(x).sqrMagnitude)
                .First();
        }

        public void OnEnter(AnimalStateManager manager)
        {
            _tileRegistry = Object.FindObjectOfType<TileRegistry>();

            var tilemap = GameObject.Find("Top").GetComponent<Tilemap>();

            var cellPosition = FindTargetRock(tilemap);
            if (cellPosition == null)
            {
                return;
            }

            _cellPosition = cellPosition.Value;
            _position = tilemap.CellToWorld(_cellPosition);
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            throw new System.NotImplementedException();
        }
    }
}