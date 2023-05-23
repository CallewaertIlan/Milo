using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestBar : MonoBehaviour
{
    [SerializeField] private Image loadBarImage;

    public void EditSize(float pourcent)
    {
        loadBarImage.transform.localScale = new Vector3(pourcent / 100, 1, 1);
    }
}
