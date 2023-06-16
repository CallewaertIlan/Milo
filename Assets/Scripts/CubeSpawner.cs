using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject CubeA;
    public float CubeTime = 1.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CubeA.SetActive(true);
            StartCoroutine("HideCube");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CubeA.activeInHierarchy)
        {
            if (other.gameObject.CompareTag("Cube"))
            {
                Destroy(other.gameObject);
                print("Cube DEAD");
            }
        }
    }

    IEnumerator HideCube()
    {
        yield return new WaitForSeconds(CubeTime);
        CubeA.SetActive(false);
    }
}