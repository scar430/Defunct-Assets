using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefunctLib.UI
{
    public class billBoard : MonoBehaviour
    {
        public Transform player;
        void Update()
        {
            transform.LookAt(2 * transform.position - player.position);
        }
    }
}
