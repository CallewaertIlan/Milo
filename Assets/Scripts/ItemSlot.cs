using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private Image slotImg;

    [SerializeField] private Equipement.EquipementType slotIndex;


    private void Awake()
    {
        slotImg = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("zsdszdzdz");
        InventoryItem ii = eventData.pointerDrag.GetComponent<InventoryItem>();
        Equipement equipementItem = eventData.pointerDrag.GetComponent<Equipement>();

        Debug.Log(equipementItem);
        Debug.Log(eventData.pointerDrag.name);

        if (slotImg != null || ii != null)
        {
            ii.isOnImage = true;

            ii.transform.SetParent(transform, true);

            ii.transform.localPosition = new Vector3(0, 0, 0);

            ii.transform.localScale = new Vector3(1, ii.transform.localScale.y, 1);

            if (equipementItem != null)
                if (!EquipItem(equipementItem))
                    ii.isOnImage = false;
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