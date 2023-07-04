using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    [SerializeField] GameObject prefabFireBall;
    [SerializeField] GameObject player;
    [SerializeField] float fireRate = 1f;
    [SerializeField] int power = 5;

    private bool playerEnter = false;


    private void Update()
    {
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.z, 0);

        if (playerEnter)
        {
            RotateTowardsPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Je détecte le joueur !");
            playerEnter = true;
            InvokeRepeating("shoot", fireRate, fireRate);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("RAS");
            playerEnter = false;
            CancelInvoke("shoot");
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void shoot()
    {
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0f;
        GameObject fireBall = Instantiate(prefabFireBall, transform.position, Quaternion.identity) as GameObject;
        fireBall.GetComponent<Rigidbody>().velocity = direction.normalized * power;
    }
}