using Inventory.Crafting;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// The registry where all items are stored
    /// </summary>
    public sealed class ItemRegistry : MonoBehaviour
    {
        public Item Weed;
        public Item Stone;

        public RecipeRegistry RecipeRegistry;
        
        private void Start()
        {
            RecipeRegistry = new RecipeRegistry(this);
        }
    }
}