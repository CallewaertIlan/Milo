using UnityEngine.EventSystems;
using UnityEngine;

public class ThrowSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform player;

    [SerializeField] private ThrownResource[] thrownResources;

    public void OnDrop(PointerEventData eventData)
    {
        Ressources res = eventData.pointerDrag.GetComponent<Ressources>();
        Equipement equip = eventData.pointerDrag.GetComponent<Equipement>();

        if (res != null)
        {
            InventoryManager.Instance.Remove(res);

            ThrownResource thrownResource = GetThrownResource(res.ressourcesType);

            if (thrownResource != null)
            {
                Vector3 spawnPosition = player.transform.position + player.transform.forward * 2f;
                GameObject instantiatedResource = Instantiate(thrownResource.resourcePrefab, spawnPosition, Quaternion.identity);
                Destroy(instantiatedResource, thrownResource.decompositionTime);
            }
        }

        if (equip != null)
        {
            InventoryManager.Instance.Remove(equip);
        }

        InventoryManager.Instance.UpdateInventory();

        Debug.Log("Drop Throw");
    }

    private ThrownResource GetThrownResource(Ressources.RessourcesType resourceType)
    {
        foreach (ThrownResource thrownResource in thrownResources)
        {
            if (thrownResource.resourceType == resourceType)
            {
                return thrownResource;
            }
        }

        return null;
    }
}