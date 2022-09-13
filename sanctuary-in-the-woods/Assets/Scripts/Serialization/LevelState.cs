using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Serialization
{
    [Serializable]
    public class LevelState
    {
        [Serializable]
        public class ItemState
        {
            public readonly int SlotIndex;
            public readonly string RegistryName;
            public readonly int Count;

            public ItemState(int slotIndex, string registryName, int count)
            {
                SlotIndex = slotIndex;
                RegistryName = registryName;
                Count = count;
            }
        }
        
        [Serializable]
        public class PlayerState
        {
            public Vector3 Position;
            public List<ItemState> Inventory;
        }
        
        [Serializable]
        public class TilemapState
        {
            public PlayerState Player;
            public List<TileState> Tiles;
        }

        [Serializable]
        public struct TileState
        {
            public readonly Vector3Int Position;
            public readonly TileBase Tile;

            public TileState(Vector3Int position, TileBase tile)
            {
                Position = position;
                Tile = tile;
            }
        }
        
        public TilemapState BottomTiles;
        public TilemapState TopTiles;
    }
}