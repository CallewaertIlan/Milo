using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Recoltable : MonoBehaviour
{
    public float timeToCollect;
    public float progressTimeCollect;
    private bool onCollect;
    private float startTimeCollect;
    
    protected virtual void Start()
    {
        onCollect = false;
    }

    protected virtual void Update()
    {
        if (onCollect)
            progressTimeCollect = Time.time - startTimeCollect;

        if (progressTimeCollect >= timeToCollect)
            Take();
    }

    public void OnTake()
    {
        startTimeCollect = Time.time;
        onCollect = true;
    }
    
    public virtual void Take()
    {
        InventoryManager.Instance.AddToInventory(this);
        Equipement equip = GetComponent<Equipement>();
        Ressources res = GetComponent<Ressources>();

        if (equip != null) InventoryManager.Instance.AddToInventory(equip);
        else if (res != null) InventoryManager.Instance.AddToInventory(res);

        gameObject.SetActive(false);
    }
}
