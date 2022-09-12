using System;

namespace Inventory
{
    public struct CraftingIngredient : IEquatable<CraftingIngredient>
    {
        public string RegistryName;
        public int Count;

        public CraftingIngredient(string registryName, int count = 1)
        {
            RegistryName = registryName;
            Count = count;
        }
        
        public bool Equals(CraftingIngredient other)
        {
            return RegistryName == other.RegistryName && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            return obj is CraftingIngredient other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RegistryName, Count);
        }
    }
}