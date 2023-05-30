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


    // Start is called before the first frame update
    protected void Start()
    {
        onCollect = false;
    }

    protected void Update()
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
    
    public void Take()
    {
        gameObject.SetActive(false);
    }
}
