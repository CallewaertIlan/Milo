using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanva;
    [SerializeField] private GameObject imagePrefab;

    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private Dictionary<Item, int> inventory; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantité

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

        inventory = new Dictionary<Item, int>(); // Initialise le dictionnaire d'inventaire
    }

    // Crée un nouvel objet dans l'inventaire avec le sprite correspondant au type d'objet
    private void CreateInventoryItem(Item item)
    {
        inventory.Add(item, 1);
    }

    private Item IsOnInventory(Item item)
    {
        foreach (KeyValuePair<Item, int> kv in inventory)
        {
            Debug.Log(kv.Key.GetComponent<Item>().GetName());
            Debug.Log(item.GetComponent<Item>().GetName());
            if (kv.Key.GetComponent<Item>().GetName() == item.GetComponent<Item>().GetName()) return kv.Key;
        }
        return null;
    }

    // Ajoute un objet à l'inventaire
    public void AddToInventory(Item item)
    {
        Item itemInventory = IsOnInventory(item);
        if (itemInventory != null) inventory[itemInventory] += 1;
        else CreateInventoryItem(item);
    }

    public void UpdateInventory()
    {
        ClearCanva();

        int x = 0;
        int y = 0;

        foreach (KeyValuePair<Item, int> element in inventory)
        {
            GameObject img = GameObject.Instantiate(imagePrefab);
            img.transform.parent = inventoryCanva.transform;

            img.transform.localPosition = new Vector3(-35 + (x * 23), 10 - (y * 30), 0);
            img.transform.localScale = new Vector3(0.1933434f, 0.2796595f, 0.5140421f);

            img.GetComponent<Image>().sprite = element.Key.inventoryImage;
            img.GetComponentInChildren<Text>().text = element.Value.ToString();

            x++;
            if (x % 4 == 0)
            {
                y++;
                x = 0;
            }
        }
    }

    private void ClearCanva()
    {
        Transform parentTransform = inventoryCanva.transform;

        // Loop through each child of the parent GameObject
        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            // Get the child GameObject
            GameObject childObject = parentTransform.GetChild(i).gameObject;

            // Destroy the child GameObject if he has the good Layer
            if (childObject.tag != "Text") Destroy(childObject);
        }
    }




    // Vérifie si la quantité d'un type d'objet dans l'inventaire a dépassé la limite spécifiée
    /*    public bool HasExceededLimit(string itemType, int limit)
        {
            if (inventory.ContainsKey(itemType))
            {
                return inventory[itemType] >= limit;
            }

            return false;
        }*/

    /*    // Met à jour le nombre d'objets affichés dans l'inventaire pour le type d'objet spécifié
        private void UpdateInventoryCount(string itemType)
        {
            int inventoryIndex = GetInventoryIndexByItemType(itemType);

            if (inventoryIndex != -1)
            {
                Text inventoryText = inventoryTexts[inventoryImages[inventoryIndex].gameObject.name];
                inventoryText.text = inventory[itemType].ToString();
            }
        }*/
}