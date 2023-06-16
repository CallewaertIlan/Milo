using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum Type
    {
        WEAPON,
        HELMET,
        CHESTPLATE,
        PANTS,
        BOOTS,
    }

    [SerializeField] protected float damage;
    [SerializeField] protected float health;
    [SerializeField] protected float mana;

    [SerializeField] protected Image inventoryImage;
    [SerializeField] protected string itemName;

    [SerializeField] protected Type itemType;

    protected List<float> GetStats()
    {
        List<float> stats = new List<float>();

        if (damage != 0) stats.Add(damage);
        if (health != 0) stats.Add(health);
        if (mana != 0) stats.Add(mana);

        return stats;
    }
}
