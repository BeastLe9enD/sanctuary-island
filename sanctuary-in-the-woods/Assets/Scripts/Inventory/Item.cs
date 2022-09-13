using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public sealed class Item : ScriptableObject
    {
        public string RegistryName;
        public string Name;
        public Sprite Sprite;
        public int MaxStackSize;
    }
}