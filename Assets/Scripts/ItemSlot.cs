using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{

    [SerializeField] private GameObject[] slots;

    private void Awake()
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

        InventoryItem ii = eventData.pointerDrag.GetComponent<InventoryItem>();
        Equipement equipementItem = eventData.pointerDrag.GetComponent<Equipement>();

        if (ii != null)
        {

            if (equipementItem != null)
            {
                EquipItem(equipementItem);
                ii.isOnImage = true;

                ii.transform.localPosition = new Vector3(0, 0, 0);

                ii.transform.localScale = new Vector3(1, ii.transform.localScale.y, 1);
            }

        }

    }

    private void EquipItem(Equipement item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (item.equipementType == slots[i].GetComponent<Equipement>().equipementType)
            {
                if (InventoryManager.Instance.EquipementIsEmpty(item, i))
                {
                    InventoryManager.Instance.Equip(item, i);
                    item.transform.SetParent(slots[i].transform, true);
                    Debug.Log("Equip");
                }
                else
                {
                    Equipement lastEquipement = InventoryManager.Instance.Replace(item, i);

                    lastEquipement.transform.SetParent(transform, true);
                    lastEquipement.gameObject.SetActive(false);

                    item.transform.SetParent(slots[i].transform, true);

                    InventoryManager.Instance.AddToInventory(lastEquipement);
                    InventoryManager.Instance.UpdateInventory();

                    Debug.Log("Replace");
                }
            }
        }
    }
}