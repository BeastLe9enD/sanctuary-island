using Inventory;
using UnityEngine;

namespace Objects {
    public class DroppedItem : MonoBehaviour {
        private PlayerInventory _playerInventory;
        
        public Item Item;

        public DroppedItem(Item item) {
            Item = item;
        }

        private void Start() {
            _playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (collision.gameObject.TryGetComponent<Player>(out var player)) {
                _playerInventory.Add(new StackedItem(Item));
                Destroy(gameObject);
            }
        }
    }
}