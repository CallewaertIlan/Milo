using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flags : MonoBehaviour
{
    private static Flags instance;



    public static Flags Instance
    {
        get 
        {
            // If the instance is null, find the existing instance in the scene
            // or create a new one if it doesn't exist.
            if (instance == null)
            {
                instance = FindObjectOfType<Flags>();

                // If no instance exists in the scene, create a new GameObject and attach the singleton script to it.
                if (instance == null)
                {
                    GameObject FlagsObject = new GameObject("Flags");
                    instance = FlagsObject.AddComponent<Flags>();
                }

                // Make sure the instance persists between scene changes.
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    private void Awake()
    {
        // If an instance already exists and it's not the current instance, destroy this instance.
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    
}
