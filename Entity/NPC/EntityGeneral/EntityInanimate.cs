using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefunctLib.UI;
using DefunctLib.EventSystems;

namespace DefunctLib.Entity
{
    public class EntityInanimate : MonoBehaviour, IPickupable
    {
        public Item item;
        public Item _item
        {
            get
            {
                return item;
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public bool pickupable;
        public bool _pickupable
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
                throw new System.NotImplementedException();
            }
        }

        public void Pickup(Item item, InventoryManager inventoryManager)
        {
            List<Item> items = new List<Item>();
            items.Add(item);
            inventoryManager.ManageItems(items, null);
            Destroy(gameObject);
        }
    }
}

