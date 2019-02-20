using UnityEngine;
using DefunctLib.UI;
using System.Collections.Generic;
using DefunctLib.EventSystems;

namespace DefunctLib.Entity
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Rotational Space")]
        public Camera camera;

        public float currentSensitivity;

        public float cameraSensitivity = 100.0f;

        public Collider collider = null;

        public float clampAngle = 80.0f;
        [SerializeField] private float rotY = 0.0f;
        [SerializeField] private float rotX = 0.0f;

        [Header("Player Movement")]
        public CharacterController characterController;
        public float speed = 6.0F;
        public float jumpSpeed = 8.0F;
        public float gravity = 20.0F;
        private Vector3 moveDirection = Vector3.zero;

        [Header("Interaction")]
        private float raycastRange = 3.0f;


        void Start()
        {
            camera = GetComponentInChildren<Camera>();

            characterController = GetComponent<CharacterController>();

            currentSensitivity = cameraSensitivity;

            Vector3 rot = transform.localRotation.eulerAngles;
            rotY = rot.y;
            rotX = rot.x;
        }

        void Update()
        {
            //Player Rotational Space
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = -Input.GetAxis("Mouse Y");

            rotY += mouseX * currentSensitivity * Time.deltaTime;
            rotX += mouseY * (currentSensitivity / 2) * Time.deltaTime;

            rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

            Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
            camera.transform.rotation = localRotation;
            transform.rotation = Quaternion.Euler(0.0f, rotY, 0.0f);

            //Player Movement.
            if (characterController.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }
            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);

            //Messaging System
            
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), out hit, raycastRange))
            {
                if(hit.collider.GetComponent<IMenu>() != null)
                {
                    if(collider != hit.collider)
                    {
                        if(collider != null)
                        {
                            collider.GetComponent<IMenu>()._billBoard.SetActive(false);
                        }
                        collider = hit.collider;
                        collider.GetComponent<IMenu>()._billBoard.SetActive(true);
                    }
                    else
                    {
                        if(collider == hit.collider && collider != null)
                        {
                            if (collider.GetComponent<IMenu>()._billBoard.activeSelf == false)
                            {
                                collider.GetComponent<IMenu>()._billBoard.SetActive(true);
                            }
                        }
                    }
                }
                if(hit.collider.GetComponent<IMenu>() != null)
                {

                }
                Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            }
            else
            {
                if (collider != null)
                {
                    collider.GetComponent<IMenu>()._billBoard.SetActive(false);
                }
                collider = null;
                Debug.DrawRay(camera.transform.position, camera.transform.TransformDirection(Vector3.forward) * raycastRange, Color.yellow);
            }
        }
    }
}