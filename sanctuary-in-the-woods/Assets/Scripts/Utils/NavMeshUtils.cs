using UnityEngine;
using UnityEngine.AI;

namespace Utils {
    public static class NavMeshUtils {
        public static bool IsAccessible(Vector2 position) {
            var result = NavMesh.SamplePosition(new Vector3(position.x, position.y, 0.0f), out var hit, 1.0f, NavMesh.AllAreas);
            return hit.hit;
        }
    }
}