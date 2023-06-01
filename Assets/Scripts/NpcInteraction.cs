using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcInteraction : MonoBehaviour
{
    [SerializeField] GameObject raycastObject;
    [SerializeField] IPanel 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(raycastObject.transform.position, raycastObject.transform.TransformDirection(Vector3.forward), out hit, 5000, LayerMask.GetMask("NPC")))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
            //Debug.Log("Press F to interact");
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Interacted With NPC");

            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            //Debug.Log("Did not Hit");
        }
    }
}
