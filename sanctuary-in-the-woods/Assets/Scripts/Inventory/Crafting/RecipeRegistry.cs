using System.Collections.Generic;

namespace Inventory.Crafting
{
    public sealed class RecipeRegistry
    {
        private readonly List<ICraftingRecipe> _recipes = new();

        /// <summary>
        /// Adds an crafting recipe
        /// </summary>
        /// <param name="recipe">The recipe</param>
        private void AddRecipe(ICraftingRecipe recipe)
        {
            _recipes.Add(recipe);
        }

        public RecipeRegistry(ItemRegistry itemRegistry)
        {
            //Add recipes here
            AddRecipe(new SimpleCraftingRecipe(new StackedItem(itemRegistry.Stone), new StackedItem(itemRegistry.Weed, 2)));
        }

        /// <summary>
        /// Gets all available recipes that can be crafted from the content that the inventory contains
        /// </summary>
        /// <param name="inventory">The inventory of the player</param>
        /// <returns>The crafting recipes</returns>
        public List<ICraftingRecipe> GetMatchingRecipes(PlayerInventory inventory)
        {
            var result = new List<ICraftingRecipe>();

            foreach (var recipe in _recipes)
            {
                var ingredients = recipe.Ingredients;

                var isValid = true;
                foreach (var ingredient in ingredients)
                {
                    if (!inventory.CanRemove(ingredient))
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid && inventory.CanAdd(recipe.Output))
                {
                    result.Add(recipe);
                }
            }

            return result;
        }

        /// <summary>
        /// Crafts an item, removes the ingredients from the inventory and adds the output
        /// Assumes that all ingredients are available inside the inventory
        /// </summary>
        /// <param name="inventory">The inventory of the player</param>
        /// <param name="recipe">The recipe</param>
        public void Craft(PlayerInventory inventory, ICraftingRecipe recipe)
        {
            var ingredients = recipe.Ingredients;
            foreach (var ingredient in ingredients)
            {
                inventory.Remove(ingredient);
            }

            inventory.Add(recipe.Output);
        }
    }
}