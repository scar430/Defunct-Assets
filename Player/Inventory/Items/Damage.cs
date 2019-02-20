using UnityEngine;
namespace DefunctLib.Entity
{
    public abstract class Damage : Item
    {
        [Header("Damage")]
        [Range(0.0f, 10000.0f)]
        public float damage;
    }
}