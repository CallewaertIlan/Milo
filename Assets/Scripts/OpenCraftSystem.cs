using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCraftSystem : MonoBehaviour
{
    [SerializeField] private Canvas craftCanvas;
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private bool isOpen;

    private void Start()
    {
        isOpen = false;
    }

    void Update()
    {
        bool settingsOpen = settingsCanvas.enabled;

        if (Input.GetKeyDown(KeyCode.C) && !settingsOpen)
        {
            isOpen = !isOpen;
            craftCanvas.enabled = isOpen;
        }
    }
}