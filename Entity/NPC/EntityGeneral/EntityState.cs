using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefunctLib.UI;
using DefunctLib.EventSystems;

namespace DefunctLib.Entity
{
    public class EntityState : MonoBehaviour, IKillable
    {
        [Header("Health")]
        [Range(0.0f, 1000000.0f)]
        public float maxHealth;
        public float health;
        float IKillable._health
        {
            get
            {
                return maxHealth;
            }

            set
            {
                health = maxHealth;
            }
        }

        public void AddHealth(EntityState entityState, float add)
        {
            entityState.health += add;
        }

        public void SubtractHealth(EntityState entityState, float reduce)
        {
            entityState.health -= reduce;
        }

        private void Start()
        {
            health = maxHealth;
        }

        private void Update()
        {
            if (health <= 0.0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
