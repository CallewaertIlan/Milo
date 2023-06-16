using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{

    // Fabrique une �p�e
    public void CraftSword()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // D�finit les ressources n�cessaires pour fabriquer une �p�e
        requiredResources.Add(Ressources.RessourcesType.WOOD, 3);
        requiredResources.Add(Ressources.RessourcesType.STONE, 2);
        requiredResources.Add(Ressources.RessourcesType.IRON, 1);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources n�cessaires
            ConsumeResources(requiredResources);

            // Ajoute l'�p�e � l'inventaire
            // Weapon sword = new Weapon();
            // inventoryManager.AddToInventory(sword);

            Debug.Log("�p�e fabriqu�e !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer une �p�e !");
        }
    }

    // Fabrique un casque
    public void CraftHelmet()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // D�finit les ressources n�cessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 2);
        requiredResources.Add(Ressources.RessourcesType.IRON, 3);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources n�cessaires
            ConsumeResources(requiredResources);

            // Ajoute le casque � l'inventaire
            // Helmet helmet = new Helmet();
            // inventoryManager.AddToInventory(helmet);

            Debug.Log("Casque fabriqu� !");
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

        // D�finit les ressources n�cessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 3);
        requiredResources.Add(Ressources.RessourcesType.STONE, 2);
        requiredResources.Add(Ressources.RessourcesType.IRON, 3);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources n�cessaires
            ConsumeResources(requiredResources);

            // Ajoute le plastron � l'inventaire
            // Chestplate chestplate = new Chestplate();
            // inventoryManager.AddToInventory(chestplate);

            Debug.Log("Plastron fabriqu� !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer un plastron !");
        }
    }

    // Fabrique des jambi�res
    public void CraftLeggings()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // D�finit les ressources n�cessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 3);
        requiredResources.Add(Ressources.RessourcesType.IRON, 2);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources n�cessaires
            ConsumeResources(requiredResources);

            // Ajoute les jambi�res � l'inventaire
            // Pants pants = new Pants();
            // inventoryManager.AddToInventory(pants);

            Debug.Log("Jambi�res fabriqu�es !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer des jambi�res !");
        }
    }

    // Fabrique des bottes
    public void CraftBoots()
    {
        Dictionary<Ressources.RessourcesType, int> requiredResources = new Dictionary<Ressources.RessourcesType, int>();

        // D�finit les ressources n�cessaires pour fabriquer un casque
        requiredResources.Add(Ressources.RessourcesType.WOOD, 2);
        requiredResources.Add(Ressources.RessourcesType.STONE, 1);
        requiredResources.Add(Ressources.RessourcesType.IRON, 1);

        if (HasEnoughResources(requiredResources))
        {
            // Consomme les ressources n�cessaires
            ConsumeResources(requiredResources);

            // Ajoute les bottes � l'inventaire
            // Boots boots = new Boots();
            // inventoryManager.AddToInventory(boots);

            Debug.Log("Bottes fabriqu� !");
        }
        else
        {
            Debug.Log("Pas assez de ressources pour fabriquer des bottes !");
        }
    }

    // V�rifie si le joueur dispose des ressources n�cessaires pour la fabrication d'un �quipement
    private bool HasEnoughResources(Dictionary<Ressources.RessourcesType, int> requiredResources)
    {
        foreach (KeyValuePair<Ressources.RessourcesType, int> requiredResource in requiredResources)
        {
            Ressources.RessourcesType resourceType = requiredResource.Key;
            int requiredAmount = requiredResource.Value;

            // V�rifie si le joueur ne poss�de pas suffisamment de cette ressource
            if (!InventoryManager.Instance.HasEnoughResource(resourceType, requiredAmount))
            {
                return false;
            }
        }

        // Le joueur poss�de suffisamment de toutes les ressources
        return true;
    }

    // Consomme les ressources n�cessaires pour la fabrication d'un �quipement
    private void ConsumeResources(Dictionary<Ressources.RessourcesType, int> requiredResources)
    {
        foreach (KeyValuePair<Ressources.RessourcesType, int> requiredResource in requiredResources)
        {
            Ressources.RessourcesType resourceType = requiredResource.Key;
            int requiredAmount = requiredResource.Value;

            // Consomme la quantit� sp�cifi�e de cette ressource dans l'inventaire
            InventoryManager.Instance.ConsumeResource(resourceType, requiredAmount);
        }
    }
}