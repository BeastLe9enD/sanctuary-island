using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Serialization
{
    /// <summary>
    /// A type that is responsible for serializing the level
    /// </summary>
    public class LevelSerializer : MonoBehaviour
    {
        private static readonly JsonSerializerSettings _settings = new()
        {
            Formatting = Formatting.Indented
        };
        
        private void SerializeTilemap(Tilemap tilemap, LevelState.TilemapState state)
        {
            var bounds = tilemap.cellBounds;
            
            for (var j = bounds.yMin; j < bounds.yMax; j++)
            {
                for (var i = bounds.xMin; i < bounds.xMax; i++)
                {
                    var pos = new Vector3Int(i, j, 0);

                    var tile = tilemap.GetTile(pos);
                    if (tile != null)
                    {
                        state.Tiles.Add(new LevelState.TileState(pos, tile));
                    }
                }
            }
        }

        private void DeserializeTilemap(Tilemap tilemap, LevelState.TilemapState state)
        {
            tilemap.ClearAllTiles();

            foreach (var tileState in state.Tiles)
            {
                tilemap.SetTile(tileState.Position, tileState.Tile);
            }
        }

        /// <summary>
        /// Loads a level from a given state
        /// </summary>
        /// <param name="state">The given level state</param>
        private void LoadLevel(LevelState state)
        {
            var bottomTileMap = GameObject.Find("Bottom").GetComponent<Tilemap>();
            SerializeTilemap(bottomTileMap, state.BottomTiles);
            
            var topTileMap = GameObject.Find("top").GetComponent<Tilemap>();
            SerializeTilemap(topTileMap, state.TopTiles);
        }
        
        /// <summary>
        /// Saves a level to a given state
        /// </summary>
        private LevelState SaveLevel()
        {
            var state = new LevelState();
            
            var bottomTileMap = GameObject.Find("Bottom").GetComponent<Tilemap>();
            DeserializeTilemap(bottomTileMap, state.BottomTiles);
            
            var topTileMap = GameObject.Find("top").GetComponent<Tilemap>();
            DeserializeTilemap(topTileMap, state.TopTiles);

            return state;
        }
        
        /// <summary>
        /// Loads a level from the given path
        /// </summary>
        /// <param name="path">The path</param>
        public void LoadLevelFromFile(string path)
        {
            using var fileStream = new FileStream(path, FileMode.Open);
            using var reader = new StreamReader(fileStream);
            using var jsonReader = new JsonTextReader(reader);

            var serializer = JsonSerializer.Create(_settings);
            LoadLevel(serializer.Deserialize<LevelState>(jsonReader));
        }

        /// <summary>
        /// Saves a level to a given path
        /// </summary>
        /// <param name="path"></param>
        public void SaveLevelToFile(string path)
        {
            var serializer = JsonSerializer.Create(_settings);
            
            using var fileStream = new FileStream(path, FileMode.Create);
            using var textWriter = new StreamWriter(fileStream);

            serializer.Serialize(textWriter, SaveLevel());
        }
    }
}