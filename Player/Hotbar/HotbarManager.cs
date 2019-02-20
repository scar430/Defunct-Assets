using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HotbarManager : MonoBehaviour {

    public List<GameObject> hotbars;

	// Use this for initialization
	void Start () {
        if(hotbars.Count > 0)
        {
            foreach (GameObject hotbar in hotbars)
            {
                hotbar.gameObject.SetActive(false);

                hotbar.transform.position = new Vector3(0.0f, 0.0f, 0.0f);

                Instantiate(hotbar, transform);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
