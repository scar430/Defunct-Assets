using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EntitySpawner : MonoBehaviour {

    public GameObject spawnObject;
    public float spawnTimer;

    public Transform curPlate;
    public Vector3 volume;

	// Use this for initialization
	void Start () {
        curPlate = GetComponentInParent<Transform>();
        InvokeRepeating("SpawnObject", 0.0f, spawnTimer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, 15.0f);
    }

    private void SpawnObject()
    {
        Instantiate(spawnObject);
        NavMeshHit hit;
        if (NavMesh.SamplePosition((transform.position + Random.insideUnitSphere * 10.0f), out hit, 5.0f, NavMesh.AllAreas))
        {
            spawnObject.transform.position = hit.position;
        }
    }
}
