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
<<<<<<< HEAD
            Recoltables recoltable = null;
            if (Input.GetKeyDown(KeyCode.E) && recoltableTriggered != null)
            {
                recoltable = recoltableTriggered.GetComponentInParent<Recoltables>();

/*                if (!InventoryManager.Instance.HasExceededLimit(recoltable.type.ToString(), 10))
                {*/
                recoltable.OnTake();
                recoltableTriggered = null;
/*                }
                else
                {
                    Debug.Log("Maximum limit for " + recoltable.type);
                }*/
=======
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
>>>>>>> 38a5c332129754a38130212972aea1ab715e68b9
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