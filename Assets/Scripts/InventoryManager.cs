using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    [SerializeField] private Dictionary<Ressources, int> inventoryRessources; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantité
    [SerializeField] private List<Equipement> inventoryEquipement; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantité
    [SerializeField] private Equipement[] equipements; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantité

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

        inventoryRessources = new Dictionary<Ressources, int>(); // Initialise le dictionnaire d'inventaire

        inventoryEquipement = new List<Equipement>();
        equipements = new Equipement[5];
    }

    // Crée un nouvel objet dans l'inventaire avec le sprite correspondant au type d'objet
    private void CreateInventoryItem(Ressources item)
    {
        inventoryRessources.Add(item, 1);
    }

    // Crée un nouvel objet dans l'inventaire avec le sprite correspondant au type d'objet
    private void CreateInventoryItem(Equipement item)
    {
        inventoryEquipement.Add(item);
    }

    public Ressources IsOnInventory(Ressources item)
    {
        foreach (KeyValuePair<Ressources, int> kv in inventoryRessources)
        {
            if (kv.Key.ressourcesType == item.GetComponent<Ressources>().ressourcesType) return kv.Key;
        }
        return null;
    }

    public Equipement IsOnInventory(Equipement item)
    {
        foreach (Equipement kv in inventoryEquipement)
        {
            if (kv.equipementType == item.GetComponent<Equipement>().equipementType) return kv;
        }
        return null;
    }

    // Ajoute un objet ressource à l'inventaire
    public void AddToInventory(Ressources item)
    {
        Ressources itemInventory = IsOnInventory(item);
        if (itemInventory != null) inventoryRessources[itemInventory] += 1;
        else
        {
            CreateInventoryItem(item);
            /*            if (itemInventory.itemType == Item.ItemType.EQUIPEMENT) ;
                        else CreateInventoryItem(item);*/
        }
    }

    // Ajoute un équipement à l'inventaire
    public void AddToInventory(Equipement item)
    {
        CreateInventoryItem(item);
    }

    public void UpdateInventory()
    {
        ClearCanva();

        int x = 0;
        int y = 0;

        GameObject line = GameObject.Instantiate(linePrefab);
        line.transform.SetParent(inventoryCanva.transform, false);

        foreach (KeyValuePair<Ressources, int> element in inventoryRessources)
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

        foreach (Equipement element in inventoryEquipement)
        {
            GameObject img = GameObject.Instantiate(imagePrefab);
            img.transform.SetParent(line.transform, true);
            img.transform.localScale = new Vector3(0.1933434f, 0.2796595f, 0.5140421f);

            img.GetComponent<Image>().sprite = element.inventoryImage;
            img.GetComponentInChildren<Text>().text = "";

            UnityEngine.Component component = img.AddComponent(element.GetType());

            var fields = element.GetType().GetFields();

            // Copy the values from sourceComponent to targetComponent
            foreach (var field in fields)
            {
                var value = field.GetValue(element);
                field.SetValue(component, value);
            }

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

    public void Equip(Equipement equipement, int equipementInt)
    {
        equipements[equipementInt] = equipement;
        Remove(equipement);
    }

    public bool EquipementIsEmpty(Equipement equipement, int equipementInt)
    {
        if (equipements[equipementInt] == null)
        {
            return true;
        }

        return false;
    }

    public void Remove(Ressources item)
    {
        foreach (KeyValuePair<Ressources, int> element in inventoryRessources)
        {
            Ressources itemInventory = IsOnInventory(item);

            if (itemInventory != null && element.Value <= 0)
            {
                inventoryRessources.Remove(itemInventory);
                return;
            }
            else if (itemInventory != null)
            {
                inventoryRessources[itemInventory] -= 1;
            }
        }
    }

    public bool IsEquiped(Equipement item)
    {
        foreach (Equipement equipement in equipements)
        {
            if (equipement == item) return true;
        }
        return false;
    }

    public void Remove(Equipement item)
    {
        foreach (Equipement element in inventoryEquipement)
        {
            Equipement itemInventory = IsOnInventory(item);

            if (itemInventory != null)
            {
                inventoryEquipement.Remove(itemInventory);
                return;
            }
        }
    }

    public void UnEquip(Equipement item)
    {
        int count = 0;
        foreach (Equipement equipement in equipements)
        {
            if (equipement == item)
            {
                equipements[count] = null;
                return;
            }
            count++;
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