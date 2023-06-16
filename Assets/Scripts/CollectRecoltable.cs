using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRecoltable : MonoBehaviour
{
    [SerializeField] GameObject player;
    private bool onTrigger;
    private GameObject recoltableTriggered;
    private CollectBar collectBar;

    void Start()
    {
        collectBar = FindObjectOfType<CollectBar>();
    }

    void Update()
    {
        if (onTrigger && Input.GetKeyDown(KeyCode.E) && recoltableTriggered != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, player.transform.TransformDirection(Vector3.forward), out hit, LayerMask.GetMask("Recoltable")))
            {
                Recoltable recoltable = hit.collider.GetComponent<Recoltable>();
                if (recoltable != null)
                {
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