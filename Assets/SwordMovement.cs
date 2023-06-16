using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public float speed = 5f;
    public int attackSpeed = 5;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveX, moveY);

            transform.position += new Vector3(movement.x + attackSpeed, movement.y - attackSpeed, 0) * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveX, moveY);

            transform.position += new Vector3(movement.x - attackSpeed, movement.y - attackSpeed, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveX, moveY);

            transform.position += new Vector3(movement.x - attackSpeed, movement.y + attackSpeed, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(moveX, moveY);

            transform.position += new Vector3(movement.x + attackSpeed, movement.y + attackSpeed, 0) * speed * Time.deltaTime;
        }
    }

}
