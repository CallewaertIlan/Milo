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
            canvasMap.SetActive(true);
            canvasMiniMap.SetActive(false);
        }

        if (canvasMap.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                canvasMap.SetActive(false);
                canvasMiniMap.SetActive(true);
            }
        }
    }
}
