using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jump;
    [SerializeField] private float gravity;
    
    private Rigidbody rb;
    private Transform cameraTransform;

    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        // Vector3 moveDirection = (cameraForward * verticalInput + cameraTransform.right * horizontalInput).normalized;

        Vector3 movement = new Vector3(horizontalInput, 0.0f, verticalInput);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(cameraTransform.forward * walkSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(cameraTransform.forward * walkSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }

        direction.y -= gravity * Time.deltaTime;

        /*

        if (direction.x != 0 || direction.z != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), 0.015f);
        }

        */
    }
}