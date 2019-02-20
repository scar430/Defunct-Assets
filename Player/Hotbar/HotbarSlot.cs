using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DefunctLib.Entity;

namespace DefunctLib
{
    namespace UI
    {
        public class HotbarSlot : MonoBehaviour, IPointerClickHandler
        {
            private HotbarRadialMenu hotbarRadialMenu;
            public Item item;
            private Sprite _sprite;
            public Sprite sprite
            {
                get
                {
                    return _sprite;
                }

                set
                {
                    if (item != null)
                    {
                        _sprite = item.sprite;
                        GetComponent<Image>().sprite = _sprite;
                    }
                }
            }

            private void OnEnable()
            {
                hotbarRadialMenu = GetComponentInParent<HotbarRadialMenu>();
            }

            public void OnPointerClick(PointerEventData eventData)
            {
                if (eventData.button == PointerEventData.InputButton.Left)
                {
                    //GetComponentInParent<FPCHandler>().Equip(item as Tool);
                }
            }
        }
    }
}
