using UnityEngine;
using UnityEngine.UI;

namespace Inventory.Crafting
{
    public sealed class CraftingInventory : MonoBehaviour
    {
        private Button[] _buttons;
        
        void Start()
        {
            var parentTransform = transform.GetChild(1).transform;

            var numButtons = 0;
            foreach (Transform slot in parentTransform)
            {
                ++numButtons;
            }

            _buttons = new Button[numButtons];

            var index = 0;
            foreach (Transform slot in parentTransform)
            {
                var button = slot.GetChild(0).GetComponent<Button>();

                var currentIndex = index;
                button.onClick.AddListener(() =>
                {
                    var playerInventory = FindObjectOfType<PlayerInventory>();
                    var itemRegistry = FindObjectOfType<ItemRegistry>();

                    var recipes = itemRegistry.RecipeRegistry.GetMatchingRecipes(playerInventory);
                    if (currentIndex < recipes.Count)
                    {
                        var recipe = recipes[currentIndex];

                        itemRegistry.RecipeRegistry.Craft(playerInventory, recipe);   
                    }
                });
                _buttons[index++] = button;
            }
        }
        
        void Update()
        {
            UpdateUI(); //TODO: maybe move out of loop if there is some time
        }
        
        private void UpdateUI()
        {
            var playerInventory = FindObjectOfType<PlayerInventory>();
            var itemRegistry = FindObjectOfType<ItemRegistry>();

            var recipes = itemRegistry.RecipeRegistry.GetMatchingRecipes(playerInventory);

            var index = 0;

            var parentTransform = transform.GetChild(1).transform;
            foreach (Transform slot in parentTransform)
            {
                var child = slot.GetChild(0).GetChild(0);
                
                var image = child.GetComponent<Image>();
                var text = child.GetChild(0).GetComponent<Text>();

                if (recipes.Count > index)
                {
                    var recipe = recipes[index];
                    var output = recipe.Output;
                    
                    image.enabled = true;
                    image.sprite = output.Item.Sprite;
                    text.enabled = output.Count > 1;

                    if (text.enabled)
                    {
                        text.text = output.Count.ToString();
                    }
                }
                else
                {
                    image.enabled = false;
                    image.sprite = null;
                    text.enabled = false;
                }

                ++index;
            }
        }
    }
}