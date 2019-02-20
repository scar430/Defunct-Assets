using UnityEngine;
using System.Collections.Generic;

namespace DefunctLib.Entity
{
    [CreateAssetMenu(menuName = "Weapon")]
    public class Tool : Damage
    {

        [Header("Specifications")]
        [Space]
        [Range(0.0f, 1000.0f)]
        public float range;//Take this into account when differentiating between melee and ranged.

        public bool automatic;//The difference between holding down the mouse to repeadtedly commit the action or individually clicking it.

        public Animation passiveState; //what the weapon looks like when its not being used.

        public List<Animation> actionAnimations;//Will select a random animation and use it to attack.
    }
}
