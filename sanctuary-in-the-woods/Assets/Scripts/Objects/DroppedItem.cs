using Inventory;
using UnityEngine;

namespace Objects {
    public class DroppedItem : MonoBehaviour
    {
        private Player _player;
        private PlayerInventory _playerInventory;
        
        public Item Item;

        public DroppedItem(Item item) {
            Item = item;
        }

        private void Start()
        {
            _player = FindObjectOfType<Player>();
            _playerInventory = GameObject.Find("PlayerInventory").GetComponent<PlayerInventory>();
        }

        void FixedUpdate()
        {
            var distance = Vector3.Distance(transform.position, _player.transform.position);
            if (distance <= 3.0)
            {
                _playerInventory.Add(new StackedItem(Item));
                Destroy(gameObject);
            }
        }
    }
}