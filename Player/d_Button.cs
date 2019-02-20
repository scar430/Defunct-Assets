using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefunctLib
{
    namespace UI
    {
        public class d_Button : MonoBehaviour, IPointerClickHandler
        {
            public enum b_Effect
            {
                SetActive,

            }
            

            public Transform target;
            [Space]
            public b_Effect effect;

            [Header("Only one is necessary.")]
            //transform that contains multiple transforms
            public Transform masterTransform;

            //effectedTransforms are transforms that are effected by what happens to target.
            public List<Transform> effectedTransforms;

            public void OnPointerClick(PointerEventData eventData)
            {
                switch (effect)
                {
                    case b_Effect.SetActive:
                        if(masterTransform != null)
                        {
                            foreach(Transform child in masterTransform.transform)
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                        if(effectedTransforms.Count > 0)
                        {
                            foreach(Transform child in effectedTransforms)
                            {
                                child.gameObject.SetActive(false);
                            }
                        }
                        target.gameObject.SetActive(true);
                        break;
                }
            }
        }
    }
}