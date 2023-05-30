using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private CharacterController cc;

    [SerializeField] private float jump;
    [SerializeField] private float gravity;
    [SerializeField] private Vector3 direction;

    private void Update()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            direction.y = jump;
        }

        direction.y -= gravity * Time.deltaTime;

        cc.Move(direction * Time.deltaTime);
    }
}