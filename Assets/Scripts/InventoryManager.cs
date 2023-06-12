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

    [SerializeField] private Dictionary<Ressources, int> inventoryRessources; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantit�
    [SerializeField] private Dictionary<Equipement, int> inventoryEquipement; // Dictionnaire pour stocker les objets de l'inventaire avec leur quantit�
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

        inventoryRessources = new Dictionary<Ressources, int>(); // Initialise le dictionnaire d'inventaire
        inventoryEquipement = new Dictionary<Equipement, int>(); // Initialise le dictionnaire d'inventaire

        equipements = new Equipement[5];
    }

    public bool HasEnoughResource(Ressources.RessourcesType resourceType, int amount)
    {
        // R�cup�re l'objet de ressource correspondant au type sp�cifi�
        Ressources resource = GetResourceOfType(resourceType);

        // V�rifie si la ressource n'existe pas
        if (resource == null)
        {
            return false;
        }

        // R�cup�re le nombre de cette ressource dans l'inventaire
        int resourceCount = GetResourceCount(resource);

        // V�rifie si le nombre de ressources est sup�rieur ou �gal � la quantit� sp�cifi�e
        return resourceCount >= amount;
    }

    public void ConsumeResource(Ressources.RessourcesType resourceType, int amount)
    {
        // R�cup�re l'objet de ressource correspondant au type sp�cifi�
        Ressources resource = GetResourceOfType(resourceType);

        // V�rifie si la ressource existe
        if (resource != null)
        {
            // Supprime la quantit� sp�cifi�e de cette ressource de l'inventaire
            RemoveFromInventory(resource, amount);
        }
    }

    private Ressources GetResourceOfType(Ressources.RessourcesType resourceType)
    {
        // It�re � travers les entr�es du dictionnaire d'inventaire de ressources
        foreach (KeyValuePair<Ressources, int> entry in inventoryRessources)
        {
            // R�cup�re l'objet de ressource de l'entr�e actuelle
            Ressources resource = entry.Key;

            // V�rifie si le type de ressource correspond au type sp�cifi�
            if (resource.ressourcesType == resourceType)
            {
                // Retourne l'objet de ressource correspondant
                return resource;
            }
        }

        // Aucune correspondance trouv�e, retourne null
        return null;
    }

    private int GetResourceCount(Ressources resource)
    {
        // V�rifie si la ressource existe dans le dictionnaire d'inventaire de ressources
        if (inventoryRessources.TryGetValue(resource, out int count))
        {
            // Retourne la quantit� de la ressource
            return count;
        }

        // La ressource n'existe pas dans l'inventaire, retourne 0
        return 0;
    }

    private void RemoveFromInventory(Ressources resource, int amount)
    {
        // V�rifie si la ressource existe dans le dictionnaire d'inventaire de ressources
        if (inventoryRessources.ContainsKey(resource))
        {
            // R�duit la quantit� de la ressource dans l'inventaire
            inventoryRessources[resource] -= amount;

            // V�rifie si la quantit� de ressources est inf�rieure ou �gale � 0
            if (inventoryRessources[resource] <= 0)
            {
                // Supprime la ressource du dictionnaire d'inventaire
                inventoryRessources.Remove(resource);
            }
        }
    }

    // Cr�e un nouvel objet dans l'inventaire avec le sprite correspondant au type d'objet
    private void CreateInventoryItem(Ressources item)
    {
        inventoryRessources.Add(item, 1);
    }

    // Cr�e un nouvel objet dans l'inventaire avec le sprite correspondant au type d'objet
    private void CreateInventoryItem(Equipement item)
    {
        inventoryEquipement.Add(item, 1);
    }

    private Ressources IsOnInventory(Ressources item)
    {
        foreach (KeyValuePair<Ressources, int> kv in inventoryRessources)
        {
            if (kv.Key.ressourcesType == item.GetComponent<Ressources>().ressourcesType) return kv.Key;
        }
        return null;
    }

    private Equipement IsOnInventory(Equipement item)
    {
        foreach (KeyValuePair<Equipement, int> kv in inventoryEquipement)
        {
            if (kv.Key.equipementType == item.GetComponent<Equipement>().equipementType) return kv.Key;
        }
        return null;
    }

    // Ajoute un objet ressource � l'inventaire
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

    // Ajoute un �quipement � l'inventaire
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

        foreach (KeyValuePair<Equipement, int> element in inventoryEquipement)
        {
            GameObject img = GameObject.Instantiate(imagePrefab);
            img.transform.SetParent(line.transform, true);
            img.transform.localScale = new Vector3(0.1933434f, 0.2796595f, 0.5140421f);

            img.GetComponent<Image>().sprite = element.Key.inventoryImage;
            img.GetComponentInChildren<Text>().text = "";

            UnityEngine.Component component = img.AddComponent(element.Key.GetType());

            var fields = element.Key.GetType().GetFields();

            // Copy the values from sourceComponent to targetComponent
            foreach (var field in fields)
            {
                var value = field.GetValue(element.Key);
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

    public void Remove(Equipement item)
    {
        foreach (KeyValuePair<Equipement, int> element in inventoryEquipement)
        {
            Equipement itemInventory = IsOnInventory(item);

            if (itemInventory != null)
            {
                inventoryEquipement.Remove(itemInventory);
                return;
            }
        }
    }

    public bool UnEquip(Equipement item)
    {
        int count = 0;
        foreach (Equipement equipement in equipements)
        {
            if (equipement == item)
            {
                equipements[count] = null;
                return true;
            }
            count++;
        }
        return false;
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