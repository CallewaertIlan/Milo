using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoltables : MonoBehaviour
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

    public void CancelCollect()
    {
        progressTimeCollect = 0f;
        onCollect = false;
    }

    public void OnTake()
    {
        startTimeCollect = Time.time;
        onCollect = true;
    }

    public virtual void Take()
    {
        Equipement equip = GetComponent<Equipement>();
        Ressources res = GetComponent<Ressources>();

        if (equip != null)
        {
            InventoryManager.Instance.AddToInventory(equip);
        }
        else if (res != null)
        {
            InventoryManager.Instance.AddToInventory(res);
        }

        gameObject.SetActive(false);
    }
}