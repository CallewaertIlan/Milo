using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jump;
    [SerializeField] private GameObject cameraGameObject;
    
    private Rigidbody rb;
    private Vector3 movementForward;
    private Vector3 sideMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementForward = new Vector3(cameraGameObject.transform.forward.x, 0, cameraGameObject.transform.forward.z) * verticalInput;
        sideMovement = cameraGameObject.transform.right * horizontalInput;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Courir vers l'avant ou l'arrière
            transform.Translate(movementForward * runSpeed * Time.deltaTime);
        }
        else
        {
            // Marcher vers l'avant ou l'arrière
            transform.Translate(movementForward * walkSpeed * Time.deltaTime);
        }

        // Déplacer sur le coté 
        transform.Translate(sideMovement * walkSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }
}