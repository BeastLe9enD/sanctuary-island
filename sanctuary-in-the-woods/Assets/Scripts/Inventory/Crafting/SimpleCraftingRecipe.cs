using System.Collections.Generic;
using System.Linq;

namespace Inventory.Crafting
{
    /// <summary>
    /// A simple crafting recipe
    /// </summary>
    public sealed class SimpleCraftingRecipe : ICraftingRecipe
    {
        public StackedItem Output { get; }
        public List<StackedItem> Ingredients { get; }

        public SimpleCraftingRecipe(StackedItem output, params StackedItem[] ingredients)
        {
            Output = output;
            Ingredients = ingredients.ToList();
        }
    }
}