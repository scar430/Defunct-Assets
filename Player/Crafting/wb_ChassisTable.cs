using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DefunctLib.EventSystems;

namespace DefunctLib
{
    namespace Entity
    {
        public class wb_ChassisTable : WorkbenchObject, IMenu
        {
            public List<GameObject> buttons;
            public List<GameObject> _buttons
            {
                get
                {
                    return buttons;
                }
            }

            public GameObject billBoard;
            public GameObject _billBoard
            {
                get
                {
                    return billBoard;
                }
            }
        }
    }
}
