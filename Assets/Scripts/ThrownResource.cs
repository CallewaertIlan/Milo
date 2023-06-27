using UnityEngine;

[CreateAssetMenu(fileName = "New Thrown Resource", menuName = "Inventory/Thrown Resource")]
public class ThrownResource : ScriptableObject
{
    public Ressources.RessourcesType resourceType;
    public GameObject resourcePrefab;
    public float decompositionTime;
}