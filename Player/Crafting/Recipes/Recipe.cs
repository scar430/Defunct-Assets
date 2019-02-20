using System.Collections.Generic;
using UnityEngine;
namespace DefunctLib.Entity
{
    [CreateAssetMenu(menuName = "Recipe")]
    public class Recipe : ScriptableObject
    {
        public Sprite sprite;
        public List<Item> requirements;
        public List<Item> results;
    }
}

