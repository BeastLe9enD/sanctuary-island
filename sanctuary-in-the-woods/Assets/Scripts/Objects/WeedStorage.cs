
using Inventory;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects
{
    public class WeedStorage : MonoBehaviour
    {
        [ItemCanBeNull] public StackedItem Slot { get; set; }
        public Rigidbody Rigidbody;
        public GameObject Player;
        public PlayerInventory PlayerInventory;
        public ItemRegistry ItemRegistry;
        public SpriteRenderer SpriteRenderer;
        public Sprite NotFilled;
        public Sprite Filled;

        public void Clear() {
            SpriteRenderer.sprite = NotFilled;
            Slot = null;
        }
        
        public bool CanAttactAnimals()
        {
            return Slot != null;
        }
    
        void Start()
        {
            Rigidbody = GetComponent<Rigidbody>();

            Player = GameObject.Find("Player");
            PlayerInventory = FindObjectOfType<PlayerInventory>();
            ItemRegistry = FindObjectOfType<ItemRegistry>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseOver() 
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (Slot != null) return;

            var toRemove = new StackedItem(ItemRegistry.Weed);

            if (PlayerInventory.CanRemove(toRemove)) {
                PlayerInventory.Remove(toRemove);
                SpriteRenderer.sprite = Filled;
            
                Slot = toRemove;
            }
        }

        void AttractAnimals() 
        {
            if (!CanAttactAnimals()) return;
        }
    
        // Update is called once per frame
        void Update()
        {
            AttractAnimals();
        }
    }

}

