using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DefunctLib.Entity;
namespace DefunctLib.UI
{
    public class HotbarRadialMenu : MonoBehaviour
    {

        //list of items in hotbar.
        public List<Item> items;

        //list of Selections.
        public List<HotbarSlot> gos;

        //Selection prefab.
        public GameObject go;

        //Where the menu starts.
        public float radialOrigin = 90.0F;

        // How far apart the selections are.
        public Vector2 offset;

        private void OnEnable()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
            RectTransform rectTransform = GetComponent<RectTransform>();
            float fOffSet = (((rectTransform.rect.height * canvas.scaleFactor) - rectTransform.rect.center.x) * 0.5f);
            int iOffSet = Mathf.RoundToInt(fOffSet);
            offset = new Vector2(iOffSet, iOffSet);
            DrawRadialMenu();
        }

        private void OnDisable()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            gos.Clear();
        }

        public void DrawRadialMenu()
        {
            //
            //If the hotbar has items in it.
            if (items.Count > 0)
            {
                //
                //Total amount of items in hotbar.
                float z = ((items.Count) - 1);
                //
                //Defining radial selections.
                float angle = (360 / items.Count) * Mathf.Deg2Rad;
                //
                //Defines the start of the radial menu.
                float degAlignment = radialOrigin * Mathf.Deg2Rad;

                for (int i = 0; i < items.Count; ++i)
                {
                    //Defining the measurement to incremenet by.
                    float rad = (i * angle) + degAlignment;
                    //
                    //Creating the object so it can be added to the list.
                    Instantiate(go, transform);
                    //
                    //(Adding the object to the list.)
                    gos = GetComponentsInChildren<HotbarSlot>().ToList();
                    //
                    //Calculating the x-cord for "radion".
                    float guiX = gos[i].transform.position.x + Mathf.Cos(rad) * offset.x;
                    //
                    //Calculating the y-cord  for "radion".
                    float guiY = gos[i].transform.position.y + Mathf.Sin(rad) * offset.y;
                    //
                    //The final calculated position of the radion.
                    Vector2 radion = new Vector2(guiX, guiY);
                    //
                    //Setting the radions position.
                    gos[i].transform.position = radion;

                    //Debug stuff.

                    //
                    //Reading the "rad" float.
                    Debug.Log(rad.ToString() + "rad");
                    //
                    //Seeing each incremenents x-cord.
                    Debug.Log(guiX.ToString() + " " + i.ToString() + "x");
                    //
                    //Seeing each incremenents y-cord.
                    Debug.Log(guiY.ToString() + " " + i.ToString() + "y");
                }

                for (int w = 0; w < items.Count; w++)
                {
                    gos[w].GetComponent<HotbarSlot>().item = items[w];
                }
            }
        }
    }
}
