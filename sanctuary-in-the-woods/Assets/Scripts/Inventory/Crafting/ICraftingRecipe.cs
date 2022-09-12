using System.Collections.Generic;

namespace Inventory.Crafting
{
    public interface ICraftingRecipe
    {
        /// <summary>
        /// The output of the recipe
        /// </summary>
        StackedItem Output { get; }
        
        /// <summary>
        /// The ingredients of recipe
        /// </summary>
        List<StackedItem> Ingredients { get; }
    }
}