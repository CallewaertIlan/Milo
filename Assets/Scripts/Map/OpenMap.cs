using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMap : MonoBehaviour
{
    [SerializeField] private GameObject canvasMap;
    [SerializeField] private GameObject canvasMiniMap;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenTheMap(true);
        }

        if (canvasMap.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OpenTheMap(false);
            }
        }
    }

    public void OpenTheMap(bool active)
    {
        canvasMap.SetActive(active);
        canvasMiniMap.SetActive(!active);
    }
}
