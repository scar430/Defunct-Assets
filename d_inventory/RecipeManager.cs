using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace DefunctLib
{
    namespace UI
    {
        public class RecipeManager : MonoBehaviour
        {
            [Header("Crafting")]
            public List<Recipe> recipes;
            [SerializeField] private List<RecipeSlot> recipeSlots;
            public GameObject recipeSlot;

            private void Start()
            {
                foreach (Recipe recipe in recipes)
                {
                    for (int i = 0; i < recipes.Count && recipeSlots.Count < recipes.Count; i++)
                    {
                        Instantiate(recipeSlot, transform);
                        recipeSlots = GetComponentsInChildren<RecipeSlot>().ToList();
                        recipeSlots[i].recipe = recipes[i];
                    }
                }
            }
        }
    }
}

