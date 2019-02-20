using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DefunctLib.Entity;

namespace DefunctLib.UI
{
    public class WorkbenchManager : MonoBehaviour
    {
        public WorkbenchObject workbenchObject;

        public Text title;
        public Transform population;
        public GameObject gameObject;

        private void Start()
        {
            title.text = workbenchObject.id;
        }

        private void OnEnable()
        {
            foreach(Transform child in population)
            {
                Destroy(child.gameObject);
            }
            foreach(Item child in workbenchObject.craftableItems)
            {
                Instantiate(gameObject, population);
            }
        }
    }
}