using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRecoltable : MonoBehaviour
{
    [SerializeField] GameObject cameraPlayer;
    private bool onTrigger;
    private GameObject recoltableTriggered;
    private CollectBar collectBar;

    void Start()
    {
        collectBar = FindObjectOfType<CollectBar>();
    }

    void Update()
    {
        if (onTrigger && recoltableTriggered != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cameraPlayer.transform.TransformDirection(Vector3.forward), out hit, LayerMask.GetMask("Recoltable")))
            {
                Debug.DrawRay(transform.position, cameraPlayer.transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);

                Recoltables recoltable = hit.collider.GetComponent<Recoltables>();
                if (Input.GetKeyDown(KeyCode.E) &&  recoltable!= null)
                {
                    Debug.Log("prout");
                    if (!collectBar.IsMoving())
                    {
                        recoltable.OnTake();
                        recoltableTriggered = null;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Recoltable"))
        {
            onTrigger = true;
            recoltableTriggered = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Recoltable"))
        {
            onTrigger = false;
            recoltableTriggered = null;
        }
    }
}