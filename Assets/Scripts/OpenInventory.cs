using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    [SerializeField] private Canvas inventoryCanvas;
    [SerializeField] private Canvas settingsCanvas;
    private bool isOpen;
    private KeyCode inventoryKey;

    private void Awake()
    {
        inventoryKey = KeyCode.I;
    }

    void Start()
    {
        isOpen = false;
        SettingsManager.Instance.OnKeyChanged += UpdateKeys;
    }

    void Update()
    {
        bool settingsOpen = settingsCanvas.enabled;

        if (Input.GetKeyDown(inventoryKey) && !settingsOpen)
        {
            InventoryManager.Instance.UpdateInventory();
            isOpen = !isOpen;
            inventoryCanvas.enabled = isOpen;
        }
    }

    private void UpdateKeys(KeyCode newForwardKey, KeyCode newBackKey, KeyCode newRightKey, KeyCode newLeftKey, KeyCode newInventoryKey)
    {
        inventoryKey = newInventoryKey;
    }
}