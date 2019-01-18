using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using DefunctLib.Entity;

public enum BehaviorType
{
    calm,
    alert,
    guarded,
    aggressive,
    scared,
}

public enum Stance
{
    prone,
    crouched,
    standing,
}

public class HumanCombatantChronie : MonoBehaviour {

    //EntityState component
    public EntityState entityState;

    //An extended version of transform.forward, used for DEBUGGING purposes.
    public Vector3 wayPoint = new Vector3(0, 10, 0);

    //NavMeshAgent Component
    public NavMeshAgent agent;

    //emotional state
    public BehaviorType behaviorType;

    //Enemies within provoking distance.
    public List<Transform> targets;

    //Field of Vision, (this is multiplied by 2)
    float FOV = 90.0f;

    //Distance that concerns the entity (measured in meters.) (The entity will take cover, spot targets, and engage in activities within this radius.)
    [Range(0.0f, 1000.0f)]
    public float maxDistance;

    //The Dot value you that will compared when finding suitable cover
    public float coverValue;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        entityState = GetComponent<EntityState>();
    }

    //Debug Circle that shows cover point
    void DrawCircle(Vector3 center, float radius, Color color)
    {
        Vector3 prevPos = center + new Vector3(radius, 0, 0);
        for (int i = 0; i < 30; i++)
        {
            float angle = (float)(i + 1) / 30.0f * Mathf.PI * 2.0f;
            Vector3 newPos = center + new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            Debug.DrawLine(prevPos, newPos, color);
            prevPos = newPos;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        float visionAngle = Vector3.Angle(transform.position, transform.forward);

        Transform target = targets.Min();

        //Is there a target at all? If so, engage.
        if (target != null)
        {
            

            //Target Position
            Vector3 targetPos = target.position - transform.position;

            //Measuring the angle of the players position in relation to the front of this obejct.
            float angle = Vector3.Angle(targetPos, transform.forward);

            //Is the player within the FOV?
            if (angle < FOV)
            {
                //Debug line of sight
                Debug.DrawLine(transform.position + transform.up, target.transform.position, Color.green);

                //RaycastHit variable
                RaycastHit rHit;

                //Draw a line of sight and test if the player is in it.
                if(Physics.Linecast(transform.position + transform.up, target.transform.position, out rHit))
                {
                    //Is the target visible?
                    if(rHit.collider.tag == "Player")
                    {

                        //If they can be seen, look at them and follow them.
                        transform.LookAt(target);

                        
                        NavMeshHit hit;

                        //Where is the closest cover zone?
                        NavMesh.FindClosestEdge(agent.destination, out hit, NavMesh.AllAreas);

                        //Is the target on the opposite side of the cover? If so then the current position is sufficient.
                        if (Vector3.Dot(hit.normal, target.transform.position - hit.position) > coverValue)
                        {
                            //Caluclating a vicinity around the player in which the sampling for possible cover canidates will begin.
                            Vector3 randomPoint = agent.destination + Random.insideUnitSphere * 10.0f;
                            NavMeshHit aHit;

                            //Find a random position
                            if (NavMesh.SamplePosition(randomPoint, out aHit, 1.0f, NavMesh.AllAreas))
                            {
                                //The result of the sampling.
                                Vector3 result = aHit.position;
                                NavMeshHit bHit;

                                //Debug.DrawLine(new Vector3(aHit.position.x, 0.0f, aHit.position.z), new Vector3(aHit.position.x, 0.0f, aHit.position.z) + (transform.up * 10), Color.green);

                                //What's the closest cover zone?
                                if (NavMesh.FindClosestEdge(aHit.position, out bHit, NavMesh.AllAreas))
                                {
                                    Vector3 resultTwo = bHit.position;

                                    //Is the cover sufficient?
                                    if (Vector3.Dot(bHit.normal, (target.transform.position - agent.destination)) < coverValue)
                                    {
                                        //If yes, the entity will travel to that position
                                        agent.destination = bHit.position;
                                        Debug.DrawLine(new Vector3(bHit.position.x, 0.0f, bHit.position.z), new Vector3(bHit.position.x, 0.0f, bHit.position.z) + (transform.up * 5), Color.red);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //If EntityState component is present
        if(entityState != null)
        {
            //If bloodML (Milliliters of blood) is less than or equal to maxBloodML (Maximum milliliters of blood) then the requirements for cover are looser.
            if (entityState.health <= entityState.maxHealth)
            {
                //180 degree angle
                coverValue = 0.0f;
            }
            //If bloodML (Milliliters of blood) is less than or equal to maxBloodML (Maximum milliliters of blood) then the requirements for cover are stricter.
            if (entityState.health <= (entityState.maxHealth * 0.66))
            {
                //90 degree angle
                coverValue = -0.5f;
            }
            //If bloodML (Milliliters of blood) is less than or equal to maxBloodML (Maximum milliliters of blood) then the requirements for cover are strictest.
            if (entityState.health <= (entityState.maxHealth * 0.33))
            {
                //45 degree angle
                coverValue = -0.75f;
            }
        }
    }
}
