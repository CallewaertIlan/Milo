using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Iron : Recoltable
{
    protected override void Start()
    {
        type = "Iron";
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