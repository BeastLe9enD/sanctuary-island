using System.Collections.Generic;

namespace Inventory
{
    public static class CraftingRecipes
    {
        private static List<ICraftingRecipe> _recipes = new List<ICraftingRecipe>();

        private static void AddRecipe(ICraftingRecipe recipe) => _recipes.Add(recipe);
        
        static CraftingRecipes()
        {
            AddRecipe(new SimpleCraftingRecipe(new CraftingIngredient("a"), new CraftingIngredient("b")));
        }

        private static bool HasIngredient(List<CraftingIngredient> ingredients, CraftingIngredient ingredient)
        {
            foreach (var currentIngredient in ingredients)
            {
                if (ingredient.RegistryName == currentIngredient.RegistryName && ingredient.Count >= currentIngredient.Count)
                {
                    return true;
                }
            }

            return false;
        }
        
        public static List<ICraftingRecipe> FindMatchingRecipes(List<CraftingIngredient> ingredients)
        {
            var result = new List<ICraftingRecipe>();

            foreach (var recipe in _recipes)
            {
                var recipeIngredients = recipe.Ingredients;

                var foundRecipe = true;
                foreach (var ingredient in recipeIngredients)
                {
                    if (!HasIngredient(ingredients, ingredient))
                    {
                        foundRecipe = false;
                        break;
                    }
                }

                if (foundRecipe)
                {
                    result.Add(recipe);   
                }
            }

            return result;
        }
    }
}