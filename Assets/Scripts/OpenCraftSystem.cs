using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCraftSystem : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private bool isOpen;

    private void Start()
    {
        isOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isOpen = !isOpen;
            canvas.enabled = isOpen;
        }
    }
}