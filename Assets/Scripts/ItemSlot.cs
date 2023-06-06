using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    [SerializeField] private Equipement.EquipementType slotIndex;

    public void OnDrop(PointerEventData eventData)
    {

        InventoryItem ii = eventData.pointerDrag.GetComponent<InventoryItem>();
        Equipement equipementItem = eventData.pointerDrag.GetComponent<Equipement>();

        Debug.Log(equipementItem);
        Debug.Log(eventData.pointerDrag.name);

        if (ii != null)
        {
            ii.isOnImage = false;

            ii.transform.SetParent(transform, true);

            ii.transform.localPosition = new Vector3(0, 0, 0);

            ii.transform.localScale = new Vector3(1, ii.transform.localScale.y, 1);

            if (equipementItem != null)
                ii.isOnImage = EquipItem(equipementItem);

            Debug.Log(EquipItem(equipementItem));
        }

    }

    private bool EquipItem(Equipement item)
    {
        Debug.Log("Uwu");
        if (item.equipementType == slotIndex)
        {
            return true;
        }
        else return false;
    }
}