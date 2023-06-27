using System;
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

    public bool explose = false;

    public bool test = false;

    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] public Dictionary<Ressources, int> inventoryRessources; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantité
    [SerializeField] public List<Equipement> inventoryEquipement; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantité
    [SerializeField] public Equipement[] equipements; // les equipements equipés

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

    public bool HasEnoughResource(Ressources.RessourcesType resourceType, int amount)
    {
        // Récupère l'objet de ressource correspondant au type spécifié
        Ressources resource = GetResourceOfType(resourceType);

        // Vérifie si la ressource n'existe pas
        if (resource == null)
        {
            return false;
        }

        // Récupère le nombre de cette ressource dans l'inventaire
        int resourceCount = GetResourceCount(resource);

        // Vérifie si le nombre de ressources est supérieur ou égal à la quantité spécifiée
        return resourceCount >= amount;
    }

    public void ConsumeResource(Ressources.RessourcesType resourceType, int amount)
    {
        // Récupère l'objet de ressource correspondant au type spécifié
        Ressources resource = GetResourceOfType(resourceType);

        // Vérifie si la ressource existe
        if (resource != null)
        {
            // Supprime la quantité spécifiée de cette ressource de l'inventaire
            RemoveFromInventory(resource, amount);
        }
    }

    private Ressources GetResourceOfType(Ressources.RessourcesType resourceType)
    {
        // Itère à travers les entrées du dictionnaire d'inventaire de ressources
        foreach (KeyValuePair<Ressources, int> entry in inventoryRessources)
        {
            // Récupère l'objet de ressource de l'entrée actuelle
            Ressources resource = entry.Key;

            // Vérifie si le type de ressource correspond au type spécifié
            if (resource.ressourcesType == resourceType)
            {
                // Retourne l'objet de ressource correspondant
                return resource;
            }
        }

        // Aucune correspondance trouvée, retourne null
        return null;
    }

    private int GetResourceCount(Ressources resource)
    {
        // Vérifie si la ressource existe dans le dictionnaire d'inventaire de ressources
        if (inventoryRessources.TryGetValue(resource, out int count))
        {
            // Retourne la quantité de la ressource
            return count;
        }

        // La ressource n'existe pas dans l'inventaire, retourne 0
        return 0;
    }

    private void RemoveFromInventory(Ressources resource, int amount)
    {
        // Vérifie si la ressource existe dans le dictionnaire d'inventaire de ressources
        if (inventoryRessources.ContainsKey(resource))
        {
            // Réduit la quantité de la ressource dans l'inventaire
            inventoryRessources[resource] -= amount;

            // Vérifie si la quantité de ressources est inférieure ou égale à 0
            if (inventoryRessources[resource] <= 0)
            {
                // Supprime la ressource du dictionnaire d'inventaire
                inventoryRessources.Remove(resource);
            }
        }
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

            // Ajoute le script correspondant à la ressource ramassée
            Ressources ressource = element.Key;
            Type scriptType = GetRessourceScriptType(ressource);
            UnityEngine.Component component = img.AddComponent(scriptType);

            var fields = scriptType.GetFields();

            // Copy the values from sourceComponent to targetComponent
            foreach (var field in fields)
            {
                var value = field.GetValue(ressource);
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

    private Type GetRessourceScriptType(Ressources ressource)
    {
        switch (ressource.ressourcesType)
        {
            case Ressources.RessourcesType.WOOD:
                return typeof(Wood);
            case Ressources.RessourcesType.IRON:
                return typeof(Iron);
            case Ressources.RessourcesType.STONE:
                return typeof(Stone);
            default:
                return null;
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

    public Equipement Replace(Equipement equipement, int equipementInt)
    {
        Equipement lastEquipement = equipements[equipementInt];
        Equip(equipement, equipementInt);
        return lastEquipement;
    }

    public bool EquipementIsEmpty(Equipement equipement, int equipementInt)
    {
        if (equipements[equipementInt] == null)
        {
            return true;
        }

        return false;
    }

    public void Remove(Ressources res)
    {
        Debug.Log("Test 1");
        Ressources itemInventory = IsOnInventory(res);

        if (itemInventory != null)
        {
            inventoryRessources[itemInventory] -= 1;

            if (inventoryRessources[itemInventory] <= 0)
            {
                inventoryRessources.Remove(itemInventory);
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