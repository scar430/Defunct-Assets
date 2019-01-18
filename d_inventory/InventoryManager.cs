using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
namespace DefunctLib
{
    namespace UI
    {
        public class InventoryManager : MonoBehaviour
        {
            private UiManager uiManager;
            [Header("Crafting")]
            public List<Recipe> recipes;
            [Header("Inventory")]
            public List<Item> items;
            public List<ItemSlot> itemSlots;
            public GameObject itemSlot;
            public int totalWeight;
            [Header("Hotbar")]
            public HotbarRadialMenu hotbarRadialMenu;

            private void Start()
            {
                uiManager = GetComponentInParent<UiManager>();
            }

            public void ManageItems(List<Item> addItems, List<Item> removeItems)
            {
                //Items to add.
                if (addItems != null)
                {
                    foreach (Item item in addItems)
                    {
                        items.Add(item);
                        Debug.Log("add items");
                        ItemSlot _itemSlot = itemSlots.Find(itemSlot => itemSlot._items[0] == item);
                        if (_itemSlot != null)
                        {
                            _itemSlot._items.Add(item);
                            _itemSlot.GetComponentInChildren<Text>().text = _itemSlot._items.Count.ToString();
                            _itemSlot = null;
                        }
                        else
                        {
                            Instantiate(itemSlot, transform);
                            itemSlots = GetComponentsInChildren<ItemSlot>().ToList();
                            itemSlots[itemSlots.Count - 1]._items.Add(item);
                            itemSlots[itemSlots.Count - 1].GetComponentInChildren<Text>().text = itemSlots[itemSlots.Count - 1]._items.Count.ToString();
                        }
                    }
                }
                //Items to remove
                if (removeItems != null)
                {
                    foreach (Item item in removeItems)
                    {
                        foreach (ItemSlot slot in itemSlots.ToArray())
                        {
                            if (slot._items[0] == item)
                            {
                                Debug.Log("1");
                                slot._items.Remove(item);
                                Debug.Log("2");
                                if (slot._items.Count == 0)
                                {
                                    Debug.Log("3");
                                    Destroy(slot.gameObject);
                                    itemSlots.Remove(slot);
                                    Debug.Log("4");
                                }
                                Debug.Log("5");
                            }
                            Debug.Log("6");
                            slot.GetComponentInChildren<Text>().text = slot._items.Count.ToString();
                        }
                        Debug.Log("7");
                        items.Remove(item);
                        Debug.Log("8");
                    }
                }
            }
        }
    }
}

