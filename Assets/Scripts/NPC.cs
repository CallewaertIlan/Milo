using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] Text interactionText;

    public enum NpcType { Classic, Blacksmith, Mage, Merchand };
    
    NpcType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InteractionBehaviour()
    {

    }

}
