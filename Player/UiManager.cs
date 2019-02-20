using UnityEngine;
using DefunctLib.Entity;
using UnityEngine.UI;
namespace DefunctLib
{
    namespace UI
    {
        public class UiManager : MonoBehaviour
        {
            [Header("Player")]
            [SerializeField]private PlayerController cameraState;
            [SerializeField]private EntityState entityState;
            [SerializeField]private Player entityAnimate;
            [Header("Menus")]
            public Transform inventoryPanel;
            public Text health;
            public Text fatigue;
            public Text hunger;
            public Text thirst;
            public Text temperature;
            [Header("Inventory")]
            public InventoryManager inventoryManager;
            public RecipeManager recipeManager;
            public HotbarRadialMenu hotbarRadialMenu;

            // Use this for initialization
            void Start()
            {
                cameraState = transform.root.GetComponent<PlayerController>();
                entityState = transform.root.GetComponent<EntityState>();
                StopMouse();
                entityAnimate = transform.root.GetComponent<Player>();
            }

            // Update is called once per frame
            void Update()
            {
                if (Input.GetButtonUp("Info"))
                {
                    //Is the Inventory open?
                    if(inventoryPanel.gameObject.activeInHierarchy == false)
                    {
                        //If no, open it.
                        inventoryPanel.gameObject.SetActive(true);
                        StartMouse();
                    }
                    else
                    if(inventoryPanel.gameObject.activeInHierarchy == true)
                    {
                        //If yes, close it.
                        inventoryPanel.gameObject.SetActive(false);
                        StopMouse();
                    }
                }

                health.text = (entityState.health.ToString() + " / " + entityState.maxHealth.ToString());
                fatigue.text = (entityAnimate.fatigue.ToString() + " / " + entityAnimate.maxFatigue.ToString());
                hunger.text = (entityAnimate.hunger.ToString() + " / " + entityAnimate.maxHunger.ToString());
                thirst.text = (entityAnimate.thirst.ToString() + " / " + entityAnimate.maxThirst.ToString());
            }

            //Default state, usually when a player is not in a menu.
            public void StopMouse()
            {
                cameraState.currentSensitivity = cameraState.cameraSensitivity;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            //Usually used in menus
            public void StartMouse()
            {
                cameraState.currentSensitivity = 0.0f;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
}