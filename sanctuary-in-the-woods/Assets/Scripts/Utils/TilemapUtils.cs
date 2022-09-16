using UnityEngine;
using UnityEngine.Tilemaps;

namespace Utils {
    public static class TilemapUtils {
        public static (Vector3Int, Vector2) GetTileOnMouse(Tilemap tilemap) {
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return (tilemap.WorldToCell(mouseWorldPos), new Vector2(mouseWorldPos.x, mouseWorldPos.y));
        }
    }
}