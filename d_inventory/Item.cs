using UnityEngine;
namespace DefunctLib
{
    namespace UI
    {
        [CreateAssetMenu(menuName = "Item")]
        public class Item : InventoryObject
        {
            public string description;
            public int weight;
            public Animation animation;
            public GameObject gameObject;
        }
    }
}

