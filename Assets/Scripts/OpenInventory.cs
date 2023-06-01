using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] bool isOpen;

    void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOpen = !isOpen;
            canvas.enabled = isOpen;
            InventoryManager.Instance.UpdateInventory();
        }
    }
}