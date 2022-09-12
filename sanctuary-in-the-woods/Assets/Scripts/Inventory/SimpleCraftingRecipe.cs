using System.Collections.Generic;

namespace Inventory
{
    public sealed class SimpleCraftingRecipe : ICraftingRecipe
    {
        public CraftingIngredient Output { get; }
        public List<CraftingIngredient> Ingredients { get; }

        public SimpleCraftingRecipe(CraftingIngredient output, List<CraftingIngredient> ingredients)
        {
            Output = output;
            Ingredients = ingredients;
        }

        public SimpleCraftingRecipe(CraftingIngredient output, CraftingIngredient ingredient)
        {
            Output = output;
            Ingredients = new List<CraftingIngredient> { ingredient };
        }
    }
}