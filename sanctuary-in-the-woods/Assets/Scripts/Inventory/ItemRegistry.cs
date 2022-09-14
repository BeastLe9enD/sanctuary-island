using Inventory.Crafting;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// The registry where all items are stored
    /// </summary>
    public sealed class ItemRegistry : MonoBehaviour
    {
        public Item Stone;
        public Item Weed;
        public Item WeedFeed;
        public Item Berries;
        public Item BerryFeed;
        public Item Seeds;
        public Item SeedFeed;
        public Item Wood;
        public Item Cake;
        public Item Bag;

        public RecipeRegistry RecipeRegistry;
        
        private void Start()
        {
            RecipeRegistry = new RecipeRegistry(this);
        }
    }
}