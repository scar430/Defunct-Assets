using UnityEngine;
using DefunctLib.UI;
using System.Collections.Generic;


namespace DefunctLib
{
    namespace Entity
    {
        public class CameraState : MonoBehaviour
        {
            [Header("Player Rotational Space")]
            public float currentSensitivity;

            public float cameraSensitivity = 100.0f;

            public float clampAngle = 80.0f;
            private float rotY = 0.0f;
            private float rotX = 0.0f;

            [Header("Player Movement")]
            public CharacterController characterController;
            public float speed = 6.0F;
            public float jumpSpeed = 8.0F;
            public float gravity = 20.0F;
            private Vector3 moveDirection = Vector3.zero;

            [Header("Interaction")]
            private float raycastRange = 3.0f;

            [Header("Inventory")]
            public InventoryManager inventoryManager;


            void Start()
            {
                characterController = GetComponentInParent<CharacterController>();

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
                rotX += mouseY * currentSensitivity * Time.deltaTime;

                rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

                Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
                transform.rotation = localRotation;

                //Player Movement.
                if (characterController.isGrounded)
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                    moveDirection = transform.TransformDirection(moveDirection);
                    moveDirection *= speed;
                    if (Input.GetButton("Jump"))
                        moveDirection.y = jumpSpeed;

                }
                moveDirection.y -= gravity * Time.deltaTime;
                characterController.Move(moveDirection * Time.deltaTime);
                
                //Messaging System
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastRange))
                {
                    Debug.Log("Pickup deployed");
                    if(hit.collider.tag == "entity")
                    {
                        if (hit.collider.GetComponent<IPickupable>() != null)
                        {
                            if (Input.GetButton("Pickup"))
                            {
                                IPickupable pickupable;
                                pickupable = hit.collider.GetComponent<IPickupable>();
                                pickupable.Pickup(pickupable._item, inventoryManager);
                            }
                        }
                    }
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastRange, Color.yellow);
                }
            }
        }
    }
}