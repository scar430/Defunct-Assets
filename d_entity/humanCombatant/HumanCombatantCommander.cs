using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCombatantCommander : MonoBehaviour {

    public List<GameObject> cronies;

    public float radialOrigin = 90.0f;
    public Vector3 offset = new Vector3(1.0f, 1.0f, 1.0f);

    //Implement use for stats

	// Use this for initialization
	void Start () {
        //Spawn cronies
        DrawSpawningRadius();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DrawSpawningRadius()
    {
        
        //If the commander has cronies.
        if (cronies.Count > 0)
        {
            
            //Defining radial selections.
            float angle = (360 / cronies.Count) * Mathf.Deg2Rad;
            
            //Defines the start of the radial menu.
            float degAlignment = radialOrigin * Mathf.Deg2Rad;

            for (int i = 0; i < cronies.Count; ++i)
            {
                //Defining the measurement to incremenet by.
                float rad = (i * angle) + degAlignment;
                
                //Creating the object so it can be added to the list.
                Instantiate(cronies[i]);
                cronies[i].transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                
                //Calculating the x-cord for "radian".
                float spawnX = transform.position.x + cronies[i].transform.position.x + Mathf.Cos(rad) * offset.x;
                
                //Calculating the y-cord  for "radian".
                float spawnZ = transform.position.z + cronies[i].transform.position.z + Mathf.Sin(rad) * offset.z;
                
                //The final calculated position of the radian.
                Vector3 radian = new Vector3(spawnX, transform.position.y, spawnZ);
                
                //Setting the radians position.
                cronies[i].transform.position = radian;
            }
        }
    }
}
