using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntitySpawner : MonoBehaviour {

    public GameObject spawnObject;
    public int quantity = 3;
    public float spawnTimer;
    public float volume;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnObject", 0.0f, spawnTimer);
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, volume);
    }

    private void SpawnObject()
    {
        for(int i = 0; i < quantity; i++)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position + (Random.insideUnitSphere * volume), out hit, volume, NavMesh.AllAreas))
            {
                Instantiate(spawnObject);
                spawnObject.transform.position = hit.position;
            }
        }
    }
}
