using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        EQUIPEMENT,
        RESSOURCES,
    }

    public Sprite inventoryImage;
    public string itemName;

    [HideInInspector] public ItemType itemType;

    public string GetName()
    {
        return itemName;
    }
}
