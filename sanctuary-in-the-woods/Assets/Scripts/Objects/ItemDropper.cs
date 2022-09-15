using System;
using Inventory;
using UnityEngine;
using Util;
using URandom = Unity.Mathematics.Random;

namespace Objects {
    public class ItemDropper : MonoBehaviour {
        public Item Item;
        public int Count;
        public float DropRadius;

        private Vector3 CreatePosition(Vector3 old) {
            var random = new URandom();
            random.InitState((uint)DateTime.Now.Ticks);
            
            Vector3 position;

            do {
                position = old;
                position.x += random.NextFloat(-DropRadius, DropRadius);
                position.y += random.NextFloat(-DropRadius, DropRadius);
            } while (!NavMeshUtils.IsAccessible(position));

            return position;
        }
    
        public void DropItems() {
            var random = new URandom();
            random.InitState((uint)DateTime.Now.Ticks);
            
            for (var i = 0; i < Count; i++) {
                var dropObject = new GameObject();

                dropObject.transform.localScale = new Vector3(0.05f, 0.05f, 1.0f);
                dropObject.transform.position = CreatePosition(transform.position);
 
                var droppedItem = dropObject.AddComponent<DroppedItem>();
                droppedItem.Item = Item;

                var spriteRenderer = dropObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Item.Sprite;

                var boxCollider2d = dropObject.AddComponent<BoxCollider2D>();
            } 
        }
    }
}