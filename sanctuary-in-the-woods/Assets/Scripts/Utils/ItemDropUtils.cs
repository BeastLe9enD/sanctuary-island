using Inventory;
using Objects;
using UnityEngine;

namespace Utils
{
    public static class ItemDropUtils
    {
        private static Vector3 CreatePosition(Vector3 old, float dropRadius)
        {
            var random = RamdomUtils.GetRandom();
            
            Vector3 position;

            do {
                position = old;
                position.x += random.NextFloat(-dropRadius, dropRadius);
                position.y += random.NextFloat(-dropRadius, dropRadius);
            } while (!NavMeshUtils.IsAccessible(position));

            return position;
        }
        
        public static void DropItems(Vector3 position, StackedItem stack, float dropRadius)
        {
            var random = RamdomUtils.GetRandom();

            for (var i = 0; i < stack.Count; i++) {
                var dropObject = new GameObject
                {
                    transform =
                    {
                        localScale = new Vector3(0.05f, 0.05f, 1.0f),
                        position = CreatePosition(position, dropRadius)
                    }
                };

                var droppedItem = dropObject.AddComponent<DroppedItem>();
                droppedItem.Item = stack.Item;

                var spriteRenderer = dropObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = stack.Item.Sprite;

                var boxCollider2d = dropObject.AddComponent<BoxCollider2D>();
            } 
        }
    }
}