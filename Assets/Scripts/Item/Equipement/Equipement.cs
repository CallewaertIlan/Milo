using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipement : Item
{
    public enum EquipementType
    {
        WEAPON,
        HELMET,
        CHESTPLATE,
        PANTS,
        BOOTS,
    }

    public EquipementType equipementType;

    [SerializeField] protected float damage;
    [SerializeField] protected float health;
    [SerializeField] protected float mana;


    // Start is called before the first frame update
    void Start()
    {
        itemType = ItemType.EQUIPEMENT;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected Dictionary<EquipementType, float> GetStats()
    {
        Dictionary<EquipementType, float> stats = new Dictionary<EquipementType, float>();

        if (damage != 0) stats.Add(equipementType, damage);
        if (health != 0) stats.Add(equipementType, health);
        if (mana != 0) stats.Add(equipementType, mana);

        return stats;
    }
}
