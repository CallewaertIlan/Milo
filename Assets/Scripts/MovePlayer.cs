using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private CharacterController cc;

    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private Vector3 direction = Vector3.zero;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            direction = new Vector3(Input.GetAxis("Horizontal") * runSpeed, direction.y, Input.GetAxis("Vertical") * runSpeed);
        }
        else
        {
            direction = new Vector3(Input.GetAxis("Horizontal") * walkSpeed, direction.y, Input.GetAxis("Vertical") * walkSpeed);
        }

        direction.y -= gravity * Time.deltaTime;

        cc.Move(direction * Time.deltaTime);
    }
}