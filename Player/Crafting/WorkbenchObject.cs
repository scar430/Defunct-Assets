using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefunctLib
{
    namespace Entity
    {
        public abstract class WorkbenchObject : MonoBehaviour
        {
            //id is the name of the Workbench
            public string id;
            //craftableItems is the items that can be crafted with this workbench.
            public List<Item> craftableItems;
        }
    }
}
