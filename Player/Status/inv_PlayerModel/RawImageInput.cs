using UnityEngine;
using UnityEngine.EventSystems;

public class RawImageInput : MonoBehaviour {

    public GameObject model;
    public float rotationSpeed = 1.0f;

    private void Awake()
    {
        model = GetComponentInChildren<MeshRenderer>().gameObject;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //if x is changed to Input.GetAxis("Mouse Y") it can roll forward
            model.transform.Rotate(0, -Input.GetAxis("Mouse X"), 0);
        }
    }
}
