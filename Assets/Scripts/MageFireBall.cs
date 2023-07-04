using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageFireBall : MonoBehaviour
{
    private float destructionDelay = 2f;

    private void Update()
    {
        Destroy(gameObject, destructionDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}