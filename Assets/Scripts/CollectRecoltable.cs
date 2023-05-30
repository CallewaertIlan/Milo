using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRecoltable : MonoBehaviour
{
    private bool onTrigger;
    private GameObject recoltableTriggered;

    void Start()
    {
        
    }

    void Update()
    {
        if (onTrigger)
        {
            Recoltable recoltable = null;
            if (Input.GetKeyDown(KeyCode.E) && recoltableTriggered != null)
            {
                recoltable = recoltableTriggered.GetComponentInParent<Recoltable>();
                if (!InventoryManager.Instance.HasExceededLimit(recoltable.type, 10))
                {
                    recoltable.OnTake();
                    recoltableTriggered = null;
                }
                else
                {
                    Debug.Log("Maximum limit for " + recoltable.type);
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
