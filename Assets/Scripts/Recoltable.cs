using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Recoltable : MonoBehaviour
{
    [SerializeField] private float timeToCollect;
    private bool onCollect;
    private float progressTimeCollect;
    private float startTimeCollect;


    // Start is called before the first frame update
    void Start()
    {
        onCollect = false;
    }
    
    void Update()
    {
        if (onCollect)
        {
            progressTimeCollect = Time.time - startTimeCollect;

            Debug.Log("Time : " + progressTimeCollect + " / " + timeToCollect);
        }

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
