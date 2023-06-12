using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressources : Item
{
    public enum RessourcesType
    {
        WOOD,
        IRON,
        STONE,
    }

    public RessourcesType ressourcesType;

    // Start is called before the first frame update
    void Awake()
    {
        itemType = ItemType.RESSOURCES;
    }

    // Update is called once per frame
    void Update()
    {

    }
}