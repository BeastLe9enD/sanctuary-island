using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;
using Object = UnityEngine.Object;

namespace Animals.Bears
{
    public sealed class BearDestroyRockState : IAnimalState
    {
        private ItemRegistry _itemRegistry;
        private TileRegistry _tileRegistry;
        private Tilemap _tilemap;

        private List<Vector3Int> _cellPositions;

        private List<Vector3Int> FindTargetRocks(Vector3 bearPos, Tilemap tilemap)
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

            var ordered = availableTiles.OrderBy(x => Vector3.Distance(bearPos, tilemap.CellToWorld(x)));

            var result = new List<Vector3Int>();

            var numRocks = ordered.Count();
            if (numRocks > 3)
            {
                numRocks = 3;
            }

            for (var i = 0; i < numRocks; i++)
            {
                result.Add(ordered.ElementAt(i));
            }

            return result;
        }

        public void OnEnter(AnimalStateManager manager)
        {
            _itemRegistry = Object.FindObjectOfType<ItemRegistry>();
            _tileRegistry = Object.FindObjectOfType<TileRegistry>();

            _tilemap = GameObject.Find("TopSolid").GetComponent<Tilemap>();

            _cellPositions = FindTargetRocks(manager.transform.position, _tilemap);
            if (_cellPositions.Count == 0)
            {
                return;
            }
            
            var worldPos = _tilemap.CellToWorld(_cellPositions.Last());
            manager.Agent.SetDestination(worldPos);
        }

        public void OnFixedUpdate(AnimalStateManager manager)
        {
            Debug.Log($"UPDATING: {_cellPositions.Count}");
            
            if (manager.Agent.remainingDistance < 2.0)
            {
                Debug.Log($"REMAINING_SIZE: {manager.Agent.remainingDistance}");
                
                _tilemap.SetTile(_cellPositions.Last(), null);
                ItemDropUtils.DropItems(manager.transform.position, new StackedItem(_itemRegistry.Stone), 2.0f);

                _cellPositions.RemoveAt(_cellPositions.Count - 1);

                if (_cellPositions.Count == 0)
                {
                    Debug.Log("SWITCHED STATE");
                    manager.Switch<AnimalTamedState>();
                }
                else
                {
                    Debug.Log("NOT SWITCHING");
                    var worldPos = _tilemap.CellToWorld(_cellPositions.Last());
                    manager.Agent.SetDestination(worldPos);
                }
            }
            else
            {
                Debug.Log("NOT NEXT ENOUGH");
            }
        }
    }
}