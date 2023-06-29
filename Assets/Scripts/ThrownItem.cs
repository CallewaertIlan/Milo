using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "New Thrown Item", menuName = "Inventory/Thrown Item")]

public class ThrownItem : ScriptableObject
{
    public Item.ItemType itemType;
    public GameObject itemPrefab;
    public float decompositionTime;
}