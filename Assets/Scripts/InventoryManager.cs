using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanva;
    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private GameObject linePrefab;

    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] private Dictionary<Item, int> inventory; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantit�
    [SerializeField] private Equipement[] equipements; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantit�

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
        equipements = new Equipement[5];
    }

    // Cr�e un nouvel objet dans l'inventaire avec le sprite correspondant au type d'objet
    private void CreateInventoryItem(Item item)
    {
        inventory.Add(item, 1);
    }

    private Item IsOnInventory(Item item)
    {
        foreach (KeyValuePair<Item, int> kv in inventory)
        {
            if (kv.Key.GetComponent<Item>().GetName() == item.GetComponent<Item>().GetName()) return kv.Key;
        }
        return null;
    }

    // Ajoute un objet � l'inventaire
    public void AddToInventory(Item item)
    {
        Item itemInventory = IsOnInventory(item);
        if (itemInventory != null) inventory[itemInventory] += 1;
        else
        {
            CreateInventoryItem(item);
/*            if (itemInventory.itemType == Item.ItemType.EQUIPEMENT) ;
            else CreateInventoryItem(item);*/
        }
    }

    public void UpdateInventory()
    {
        ClearCanva();

        int x = 0;
        int y = 0;

        GameObject line = GameObject.Instantiate(linePrefab);
        line.transform.SetParent(inventoryCanva.transform, false);

        foreach (KeyValuePair<Item, int> element in inventory)
        {
            GameObject img = GameObject.Instantiate(imagePrefab);
            img.transform.SetParent(line.transform, true);
            img.transform.localScale = new Vector3(0.1933434f, 0.2796595f, 0.5140421f);

            img.GetComponent<Image>().sprite = element.Key.inventoryImage;
            img.GetComponentInChildren<Text>().text = element.Value.ToString();

            x++;
            if (x % 10 == 0)
            {
                y++;
                x = 0;

                line = GameObject.Instantiate(linePrefab);
                line.transform.SetParent(inventoryCanva.transform, false);
            }

            if (y == 7) return;
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
            if (childObject.tag == "InventoryImage") Destroy(childObject);
        }
    }

    private void Equip(Equipement equipement, int equipementInt)
    {
        if (equipements[equipementInt] == null) equipements[equipementInt] = equipement;
    }


    // V�rifie si la quantit� d'un type d'objet dans l'inventaire a d�pass� la limite sp�cifi�e
    /*    public bool HasExceededLimit(string itemType, int limit)
        {
            if (inventory.ContainsKey(itemType))
            {
                return inventory[itemType] >= limit;
            }

            return false;
        }*/

    /*    // Met � jour le nombre d'objets affich�s dans l'inventaire pour le type d'objet sp�cifi�
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