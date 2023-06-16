using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipement : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float health;
    [SerializeField] private float mana;

    private Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        SetStats();

        AddStats();
    }

    private void SetStats()
    {
        damage = 0; health = 0; mana = 0;

        foreach (Equipement equip in InventoryManager.Instance.equipements)
        {
            if (equip != null)
            {
                health += equip.health;
                mana += equip.mana;
                damage += equip.damage;
            }
        }
    }

    private void AddStats()
    {
        entity.SetDamageWithStartValue(damage);
        entity.SetMaxLifeWithStartValue(health);
        entity.SetMaxManaWithStartValue(mana);
    }
}
