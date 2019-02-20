using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using DefunctLib.Entity;

namespace DefunctLib
{
    namespace UI
    {
        public class ItemSlot : MonoBehaviour, IPointerClickHandler
        {
            public Text text;
            public UiManager uiManager;
            //private FPCHandler fPCHandler;
            [SerializeField]
            private HotbarRadialMenu hotbarRadialMenu;
            [Header("Stack")]
            public List<Item> _items;

            private void Start()
            {
                Debug.Log("Item loaded");
                text = GetComponentInChildren<Text>();
                uiManager = GetComponentInParent<UiManager>();
                //fPCHandler = GetComponentInParent<FPCHandler>();
                hotbarRadialMenu = uiManager.hotbarRadialMenu;
                Debug.Log("Item stop loading");
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                Debug.Log("Started Click");
                Debug.Log("Started click");
                //Remove Whole Stack.
                //Checks for Right-Click and Ctrl.
                if (Input.GetButton("ctrl") && eventData.button == PointerEventData.InputButton.Right)
                {
                    List<Item> items = _items.GetRange(0, _items.Count);
                    uiManager.inventoryManager.ManageItems(null, items);
                    Eject(items);
                }
                //Remove Half of the Stack.
                //Checks for Right-Click and Alt.
                if (Input.GetButton("alt") && eventData.button == PointerEventData.InputButton.Right)
                {
                    List<Item> items = _items.GetRange(0, (_items.Count / 2));
                    uiManager.inventoryManager.ManageItems(null, items);
                    Eject(items);
                }
                //Remove One Item from the Stack.
                //Checks for only Right-Click.
                if (eventData.button == PointerEventData.InputButton.Right && !Input.GetButton("ctrl") && !Input.GetButton("alt"))
                {
                    Debug.Log("Remove Item");
                    List<Item> items = new List<Item>();
                    items.Add(_items[0]);
                    uiManager.inventoryManager.ManageItems(null, items);
                    Eject(items);
                    Debug.Log("Removed, and ejected selected items");
                }
                //Add items to the Hot-Bar.
                //Checks for only Left-Click.
                if (eventData.button == PointerEventData.InputButton.Left && !Input.GetButton("shift"))
                {
                    //Retrieving the first item from the stack, also works if there only is one item.
                    Item item = _items[0];
                    //10 being the limit, if there is 10 Hot-Bar items then it is at capacity.
                    if (hotbarRadialMenu.items.Count < 10)
                    {
                        //Checking to see if the Hot-Bar contains the item already.
                        Item _item = hotbarRadialMenu.items.Find(x => x == item);
                        //If the Hot-Bar does not contain the item then add it.
                        if (_item == null)
                        {
                            if (item is Tool)
                            {
                                hotbarRadialMenu.items.Add(item);
                            }
                        }
                        //If the Hot-Bar does contain the item then do NOT add it.
                        else
                        {

                        }
                    }
                }
                //Removes items from Hot-Bar.
                //Checks for Left-Click + Shift.
                if (eventData.button == PointerEventData.InputButton.Left && Input.GetButton("shift"))
                {
                    //Retrieving the first item from the stack, also works if there only is one item.
                    Item item = _items[0];
                    //Checking to see if the Hot-Bar contains the item yet.
                    Item _item = hotbarRadialMenu.items.Find(x => x == item);
                    //If the Hot-Bar contains the item then remove it.
                    if (_item != null)
                    {
                        hotbarRadialMenu.items.Remove(item);
                    }
                    //If the Hot-Bar does not contain the item then do NOT remove it.
                    else
                    {
                    }
                }
                Debug.Log("End Click");
            }

            public void Eject(List<Item> items)
            {
                foreach(Item item in items)
                {
                    if(item.gameObject != null)
                    {
                        GameObject curObject = item.gameObject;
                        if (curObject.GetComponent<Rigidbody>() == null)
                        {
                            curObject.AddComponent<Rigidbody>();
                        }
                        if(curObject.GetComponent<EntityState>() == null)
                        {
                            
                        }
                        curObject.transform.position = GetComponentInParent<PlayerController>().transform.position + GetComponentInParent<PlayerController>().transform.forward;
                        curObject.transform.rotation = GetComponentInParent<PlayerController>().transform.rotation;
                        Instantiate(curObject);
                        Debug.Log("Dropped item");
                    }
                }
            }
        }
    }
}