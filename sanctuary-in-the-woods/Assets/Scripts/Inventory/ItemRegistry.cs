using Inventory.Crafting;
using UnityEngine;

namespace Inventory
{
    public class ItemRegistry : MonoBehaviour
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