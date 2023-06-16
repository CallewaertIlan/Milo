using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public enum NpcType { Classic, Blacksmith, Mage, Merchand };
    
    [SerializeField] NpcType type;

    private bool isInteracting;

    [SerializeField] GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InteractionBehaviour();
    }

    void InteractionBehaviour()
    {
        if (isInteracting)
        {
            panel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            panel.SetActive(false);
            isInteracting = false;
        }
    }

    public NpcType GetNpcType()
    {
        return type;
    }

    public void SetIsInteracting(bool _isInteracting) 
    {
        isInteracting = _isInteracting; 
    }

}
