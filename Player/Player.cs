using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefunctLib
{
    namespace Entity
    {
        public class Player : MonoBehaviour
        {

            [Range(0.0f, 1000000.0f)]
            public float maxFatigue;

            public float fatigue;

            [Space]
            [Range(0.0f, 1000000.0f)]
            public float maxHunger;

            public float hunger;

            [Range(0.0f, 1000000.0f)]
            public float hungerOffsetTime;

            [Range(0.0f, 1000000.0f)]
            public float hungerOffsetValue;

            [Range(0.0f, 1000000.0f)]
            public float hungerDamage;

            [Space]
            [Range(0.0f, 1000000.0f)]
            public float maxThirst;

            public float thirst;

            [Range(0.0f, 1000000.0f)]
            public float thirstOffsetTime;

            [Range(0.0f, 1000000.0f)]
            public float thirstOffsetValue;

            [Range(0.0f, 1000000.0f)]
            public float thirstDamage;

            /*//Temperature measured in degrees
            [Range(0.0f, 1000000.0f)]
            public float maxTemperature;

            [Range(-1000000.0f, 1000000.0f)]
            public float minTemperature;

            public float temperature;*/

            // S.A.V.I.O.R. stats
            [Header("S.A.V.I.O.R. Statistics")]
            [Range(0,10)]
            public int strength;

            [Range(0, 10)]
            public int agility;

            [Range(0, 10)]
            public int vitality;

            [Range(0, 10)]
            public int intelligent;

            [Range(0, 10)]
            public int obliging;

            [Range(0, 10)]
            public int reculsivity;
            
            private void Start()
            {
                hunger = maxHunger;
                thirst = maxThirst;
                InvokeRepeating("LoseHunger", hungerOffsetTime, hungerOffsetTime);
                InvokeRepeating("LoseThirst", thirstOffsetTime, thirstOffsetTime);
            }

            private void Update()
            {
                if (GetComponent<EntityState>() != null)
                {
                    EntityState entityState = GetComponent<EntityState>();
                    if (thirst <= 0.0f)
                    {
                        if (!IsInvoking("ThirstDamage"))
                        {
                            InvokeRepeating("ThirstDamage", thirstOffsetTime, thirstOffsetTime);
                        }
                    }

                    if (hunger <= 0.0f)
                    {
                        if (!IsInvoking("HungerDamage"))
                        {
                            InvokeRepeating("HungerDamage", hungerOffsetTime, hungerOffsetTime);
                        }
                    }
                }
            }

            private void LoseHunger()
            {
                if(hunger > 0.0f)
                {
                    hunger -= hungerOffsetValue;
                }
            }

            private void LoseThirst()
            {
                if (thirst > 0.0f)
                {
                    thirst -= thirstOffsetValue;
                }
            }

            private void ThirstDamage()
            {
                if(GetComponent<EntityState>() != null)
                {
                    EntityState entityState = GetComponent<EntityState>();
                    entityState.SubtractHealth(GetComponent<EntityState>(), thirstDamage);
                }
            }

            private void HungerDamage()
            {
                if (GetComponent<EntityState>() != null)
                {
                    EntityState entityState = GetComponent<EntityState>();
                    entityState.SubtractHealth(GetComponent<EntityState>(), hungerDamage);
                }
            }
        }
    }
}