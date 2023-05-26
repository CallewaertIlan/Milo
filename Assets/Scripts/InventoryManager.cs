using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private Dictionary<string, int> inventory;

    [SerializeField] private Image[] inventoryImages;
    [SerializeField] private Sprite woodSprite;
    [SerializeField] private Sprite rockSprite;

    private int firstEmptySlotIndex = 0;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        inventory = new Dictionary<string, int>();
    }

    private int GetInventoryIndexByItemType(string itemType)
    {
        switch (itemType)
        {
            case "Wood":
                return 0;
            case "Rock":
                return 1;
            default:
                return -1;
        }
    }

    private void FindNextEmptySlot()
    {
        for (int i = 0; i < inventoryImages.Length; i++)
        {
            if (inventoryImages[i].sprite == null)
            {
                firstEmptySlotIndex = i;
                break;
            }
        }
    }

    private bool InventoryIsFull()
    {
        for (int i = 0; i < inventoryImages.Length; i++)
        {
            if (inventoryImages[i].sprite == null)
            {
                return false;
            }
        }
        
        return true;
    }

    public void AddToInventory(Recoltable item)
    {
        string itemType = item.type;

        if (InventoryIsFull())
        {
            Debug.Log("Inventory Is Full");
            return;
        }

        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType]++;
        }
        else
        {
            inventory[itemType] = 1;
        }

        Debug.Log(itemType + " : " + inventory[itemType]);

        int inventoryIndex = GetInventoryIndexByItemType(itemType);

        if (inventoryIndex != -1)
        {
            Image inventoryImage = inventoryImages[firstEmptySlotIndex];

            switch (itemType)
            {
                case "Wood":
                    inventoryImage.sprite = woodSprite;
                    break;
                case "Rock":
                    inventoryImage.sprite = rockSprite;
                    break;
            }

            FindNextEmptySlot();
        }
    }
}