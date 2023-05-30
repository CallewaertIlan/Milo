using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    protected void Start()
    {
        itemType = Type.WEAPON;
    }
}
