using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using DefunctLib.Entity;
namespace DefunctLib.UI
{
    public class RecipeSlot : MonoBehaviour, IPointerClickHandler
    {
        private UiManager uiControl;
        [SerializeField] protected Image image;

        [SerializeField] protected Recipe _recipe;
        public Recipe recipe
        {
            get
            {
                return _recipe;
            }

            set
            {
                _recipe = value;
                if (_recipe != null)
                {
                    image.sprite = _recipe.sprite;
                }
            }
        }

        private void Awake()
        {
            image = GetComponent<Image>();
            uiControl = GetComponentInParent<UiManager>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                int a = 0;
                foreach (Item item in recipe.requirements)
                {
                    if (recipe.requirements.Where(y => y != null && y == item).Count() <= uiControl.inventoryManager.items.Where(x => x != null && x == item).Count())
                    {
                        Debug.Log("Competent resources.");
                        if (a == recipe.requirements.Count - 1)
                        {

                            //uiControl.generateInventory.ManageItems(recipe.results, recipe.requirements);
                            Debug.Log("Created Item");
                        }
                        else
                        {
                            a++;
                        }
                    }
                    else
                    {
                        Debug.Log("Inadequate resources.");
                        break;
                    }
                }

            }
        }
    }
}
