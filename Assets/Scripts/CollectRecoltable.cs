using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRecoltable : MonoBehaviour
{
    private bool onTrigger;
    private GameObject recoltableTriggered;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (onTrigger)
        {
            Recoltable recoltable = null;
            if (Input.GetKeyDown(KeyCode.E))
            {
                recoltable = recoltableTriggered.GetComponentInParent<Recoltable>();
                recoltable.OnTake();
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
