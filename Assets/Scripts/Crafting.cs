using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{

    // Fabrique une épée
    public void CraftSword()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // Définit les ressources nécessaires pour fabriquer une épée
        requiredResources.Add(Ressources.RessourcesType.WOOD, 3);
        requiredResources.Add(Ressources.RessourcesType.STONE, 2);
        requiredResources.Add(Ressources.RessourcesType.IRON, 1);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources nécessaires
            ConsumeResources(requiredResources);

            // Ajoute l'épée à l'inventaire
            // Weapon sword = new Weapon();
            // inventoryManager.AddToInventory(sword);

            Debug.Log("Épée fabriquée !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer une épée !");
        }
    }

    // Fabrique un casque
    public void CraftHelmet()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // Définit les ressources nécessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 2);
        requiredResources.Add(Ressources.RessourcesType.IRON, 3);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources nécessaires
            ConsumeResources(requiredResources);

            // Ajoute le casque à l'inventaire
            // Helmet helmet = new Helmet();
            // inventoryManager.AddToInventory(helmet);

            Debug.Log("Casque fabriqué !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer un casque !");
        }
    }

    // Fabrique un plastron
    public void CraftChestplate()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // Définit les ressources nécessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 3);
        requiredResources.Add(Ressources.RessourcesType.STONE, 2);
        requiredResources.Add(Ressources.RessourcesType.IRON, 3);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources nécessaires
            ConsumeResources(requiredResources);

            // Ajoute le plastron à l'inventaire
            // Chestplate chestplate = new Chestplate();
            // inventoryManager.AddToInventory(chestplate);

            Debug.Log("Plastron fabriqué !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer un plastron !");
        }
    }

    // Fabrique des jambières
    public void CraftLeggings()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // Définit les ressources nécessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 3);
        requiredResources.Add(Ressources.RessourcesType.IRON, 2);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources nécessaires
            ConsumeResources(requiredResources);

            // Ajoute les jambières à l'inventaire
            // Pants pants = new Pants();
            // inventoryManager.AddToInventory(pants);

            Debug.Log("Jambières fabriquées !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer des jambières !");
        }
    }

    // Fabrique des bottes
    public void CraftBoots()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // Définit les ressources nécessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 2);
        requiredResources.Add(Ressources.RessourcesType.STONE, 1);
        requiredResources.Add(Ressources.RessourcesType.IRON, 1);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources nécessaires
            ConsumeResources(requiredResources);

            // Ajoute les bottes à l'inventaire
            // Boots boots = new Boots();
            // inventoryManager.AddToInventory(boots);

            Debug.Log("Bottes fabriqué !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer des bottes !");
        }
    }

    // Vérifie si le joueur dispose des ressources nécessaires pour la fabrication d'un équipement
    private bool HasEnoughResources(Dictionary<Ressources.RessourcesType, int> requiredResources)
    {
        foreach (KeyValuePair<Ressources.RessourcesType, int> requiredResource in requiredResources)
        {
            Ressources.RessourcesType resourceType = requiredResource.Key;
            int requiredAmount = requiredResource.Value;

            // Vérifie si le joueur ne possède pas suffisamment de cette ressource
            if (!InventoryManager.Instance.HasEnoughResource(resourceType, requiredAmount))
            {
                return false;
            }
        }

        // Le joueur possède suffisamment de toutes les ressources
        return true;
    }

    // Consomme les ressources nécessaires pour la fabrication d'un équipement
    private void ConsumeResources(Dictionary<Ressources.RessourcesType, int> requiredResources)
    {
        foreach (KeyValuePair<Ressources.RessourcesType, int> requiredResource in requiredResources)
        {
            Ressources.RessourcesType resourceType = requiredResource.Key;
            int requiredAmount = requiredResource.Value;

            // Consomme la quantité spécifiée de cette ressource dans l'inventaire
            InventoryManager.Instance.ConsumeResource(resourceType, requiredAmount);
        }
    }
}