using UnityEngine;
using UnityEngine.AI;

namespace DefunctLib
{
    namespace Entity
    {
        public class DebugRoaming : MonoBehaviour
        {

            public NavMeshAgent navMeshAgent;

            // Use this for initialization
            void Start()
            {
                navMeshAgent = GetComponent<NavMeshAgent>();
                InvokeRepeating("Wander", 0.0f, 3.0f);
            }

            public void Wander()
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition((transform.position + Random.insideUnitSphere * 5.0f), out hit, 5.0f, NavMesh.AllAreas))
                {
                    Debug.DrawLine(hit.position + transform.up, hit.position, Color.red);
                    navMeshAgent.destination = hit.position;
                }
            }
        }
    }
}