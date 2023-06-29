using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform player;

    [SerializeField] private ThrownItem[] thrownResources;
    [SerializeField] private ThrownItem[] thrownEquipments;

    public void OnDrop(PointerEventData eventData)
    {
        Item item = eventData.pointerDrag.GetComponent<Item>();

        if (item != null)
        {
            if (item is Ressources)
            {
                Ressources resource = item as Ressources;
                ThrownItem thrownItem = GetThrownItem(Item.ItemType.RESSOURCES, resource.ressourcesType);

                if (thrownItem != null)
                {
                    Vector3 spawnPosition = player.transform.position + player.transform.forward * 2f;
                    GameObject instantiatedItem = Instantiate(thrownItem.itemPrefab, spawnPosition, Quaternion.identity);
                    Destroy(instantiatedItem, thrownItem.decompositionTime);
                }

                InventoryManager.Instance.Remove(resource);
            }
            else if (item is Equipement)
            {
                Equipement equipment = item as Equipement;
                ThrownItem thrownItem = GetThrownItem(Item.ItemType.EQUIPEMENT, equipment.equipementType);

                if (thrownItem != null)
                {
                    Vector3 spawnPosition = player.transform.position + player.transform.forward * 2f;
                    GameObject instantiatedItem = Instantiate(thrownItem.itemPrefab, spawnPosition, Quaternion.identity);
                    Destroy(instantiatedItem, thrownItem.decompositionTime);
                }

                InventoryManager.Instance.Remove(equipment);
            }
        }

        InventoryManager.Instance.UpdateInventory();

        Debug.Log("Drop Throw");
    }

    private ThrownItem GetThrownItem(Item.ItemType itemType, Ressources.RessourcesType ressourcesType)
    {
        ThrownItem[] thrownItems = null;

        if (itemType == Item.ItemType.RESSOURCES)
        {
            thrownItems = thrownResources;
        }
        else if (itemType == Item.ItemType.EQUIPEMENT)
        {
            thrownItems = thrownEquipments;
        }

        if (thrownItems != null)
        {
            foreach (ThrownItem thrownItem in thrownItems)
            {
                if (thrownItem.itemType == itemType && thrownItem.itemPrefab.GetComponent<Ressources>().ressourcesType == ressourcesType)
                {
                    return thrownItem;
                }
            }
        }

        return null;
    }

    private ThrownItem GetThrownItem(Item.ItemType itemType, Equipement.EquipementType equipementType)
    {
        ThrownItem[] thrownItems = null;

        if (itemType == Item.ItemType.RESSOURCES)
        {
            thrownItems = thrownResources;
        }
        else if (itemType == Item.ItemType.EQUIPEMENT)
        {
            thrownItems = thrownEquipments;
        }

        if (thrownItems != null)
        {
            foreach (ThrownItem thrownItem in thrownItems)
            {
                if (thrownItem.itemType == itemType && thrownItem.itemPrefab.GetComponent<Equipement>().equipementType == equipementType)
                {
                    return thrownItem;
                }
            }
        }

        return null;
    }
}