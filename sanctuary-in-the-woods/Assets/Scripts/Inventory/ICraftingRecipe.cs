using System.Collections.Generic;

namespace Inventory
{
    public interface ICraftingRecipe
    {
        CraftingIngredient Output { get; }
        List<CraftingIngredient> Ingredients { get; }
    }
}