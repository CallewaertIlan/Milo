using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get { return _instance; }
    }

    private Dictionary<string, int> inventory; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantité
    private Dictionary<string, Text> inventoryTexts; // Dictionnaire pour stocker les textes associés aux objets de l'inventaire

    [SerializeField] private Image[] inventoryImages; // Tableau d'images pour afficher visuellement les objets de l'inventaire
    [SerializeField] private Text[] inventoryCountTexts; // Tableau de textes pour afficher les quantités des objets de l'inventaire

    [SerializeField] private Sprite woodSprite; // Sprite pour l'objet "Wood"
    [SerializeField] private Sprite stoneSprite; // Sprite pour l'objet "Stone"
    [SerializeField] private Sprite ironSprite; // Sprite pour l'objet "Iron"

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

        inventory = new Dictionary<string, int>(); // Initialise le dictionnaire d'inventaire
        inventoryTexts = new Dictionary<string, Text>(); // Initialise le dictionnaire de textes

        // Associe chaque image de l'inventaire à son texte correspondant dans les dictionnaires
        for (int i = 0; i < inventoryImages.Length; i++)
        {
            inventoryTexts[inventoryImages[i].gameObject.name] = inventoryCountTexts[i];
        }
    }

    // Vérifie si la quantité d'un type d'objet dans l'inventaire a dépassé la limite spécifiée
    public bool HasExceededLimit(string itemType, int limit)
    {
        if (inventory.ContainsKey(itemType))
        {
            return inventory[itemType] >= limit;
        }

        return false;
    }

    // Renvoie l'indice de la première case vide dans l'inventaire
    private int GetEmptyInventorySlotIndex()
    {
        for (int i = 0; i < inventoryImages.Length; i++)
        {
            if (inventoryImages[i].sprite == null)
            {
                return i;
            }
        }

        return -1;
    }

    // Crée un nouvel objet dans l'inventaire avec le sprite correspondant au type d'objet
    private void CreateInventoryItem(string itemType)
    {
        int emptySlotIndex = GetEmptyInventorySlotIndex();

        if (emptySlotIndex != -1)
        {
            Image inventoryImage = inventoryImages[emptySlotIndex];

            // Associe le sprite approprié en fonction du type d'objet
            switch (itemType)
            {
                case "Wood":
                    inventoryImage.sprite = woodSprite;
                    break;
                case "Stone":
                    inventoryImage.sprite = stoneSprite;
                    break;
                case "Iron":
                    inventoryImage.sprite = ironSprite;
                    break;
            }

            Text inventoryText = inventoryTexts[inventoryImage.gameObject.name];
            inventoryText.text = "1";
            inventoryText.gameObject.SetActive(true);
        }
    }

    // Renvoie l'indice dans l'inventaire de l'objet correspondant au type d'objet spécifié
    private int GetInventoryIndexByItemType(string itemType)
    {
        for (int i = 0; i < inventoryImages.Length; i++)
        {
            if (inventoryImages[i].sprite != null)
            {
                string spriteName = inventoryImages[i].sprite.name;

                // Vérifie si le nom du sprite contient le type d'objet spécifié
                if (spriteName.Contains(itemType))
                {
                    return i;
                }
            }
        }

        return -1;
    }

    // Met à jour le nombre d'objets affichés dans l'inventaire pour le type d'objet spécifié
    private void UpdateInventoryCount(string itemType)
    {
        int inventoryIndex = GetInventoryIndexByItemType(itemType);

        if (inventoryIndex != -1)
        {
            Text inventoryText = inventoryTexts[inventoryImages[inventoryIndex].gameObject.name];
            inventoryText.text = inventory[itemType].ToString();
        }
    }

    // Ajoute un objet à l'inventaire
    public void AddToInventory(Recoltable item)
    {
        string itemType = item.type;

        if (inventory.ContainsKey(itemType))
        {
            inventory[itemType]++;
        }
        else
        {
            inventory[itemType] = 1;
            CreateInventoryItem(itemType);
        }

        UpdateInventoryCount(itemType);
    }
}