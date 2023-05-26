using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Wood : Recoltable
{
    protected override void Start()
    {
        type = "Wood";
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Take()
    {
        base.Take();
    }
}