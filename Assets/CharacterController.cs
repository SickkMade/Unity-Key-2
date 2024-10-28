using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Player Control Options")]
    [SerializeField, Range(0,100)]
    float jumpHeight = 1;
    [SerializeField, Range(0,20)]
    float walkSpeed = 5f;
    [SerializeField, Range(0, 20)]
    float runSpeed = 7.5f;
    [SerializeField, Range(0,10)]
    float lookSpeed = 3;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Camera playerCamera;
    private float speed;

    private bool isGrounded = true;
    [SerializeField]
    private LayerMask groundMask;
    private float groundCheckDistance;
    private float rotationX = 0f;

    void Start(){
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        groundCheckDistance = collider.height / 2f + .1f;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        speed = walkSpeed;
    }

    void Update(){
        isGrounded = Physics.CheckSphere(transform.position + (Vector3.down * groundCheckDistance), groundCheckDistance, groundMask);
        if(isGrounded){
            if(Input.GetButtonDown("Jump")){
                Jump();
            }

            if(Input.GetKey(KeyCode.LeftShift)){
                speed = runSpeed;
            } else{
                speed = walkSpeed;
            }
        }
       

        //walking
        Vector3 movement = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) * speed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        //left and right rot
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        
        //updown rot
        rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0 , 0);
    }

    private void Jump(){
        rb.AddForce(jumpHeight * Vector3.up, ForceMode.Impulse);
        isGrounded = false;
    }
}

