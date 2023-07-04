using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;

    [SerializeField] private Entity entity;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Text myHealth;

    [SerializeField] private Material bloodEffectMaterial;

    private void Start()
    {
        healthBar.GetComponent<Image>().color = Color.green;
        
        SetBloodIntensity(0f);
        Debug.Log("_BloodIntensity : " + bloodEffectMaterial.GetFloat("_BloodIntensity"));
    }

    private void Update()
    {
        myHealth.text = entity.life.ToString();

        healthBarImage.fillAmount = entity.life / entity.maxLife;

        if (entity.life <= entity.maxLife / 2 && entity.life >= entity.maxLife / 3)
        {
            healthBar.GetComponent<Image>().color = Color.yellow;
            
            SetBloodIntensity(0f);
            Debug.Log("_BloodIntensity : " + bloodEffectMaterial.GetFloat("_BloodIntensity"));
        }
        else if (entity.life < entity.maxLife / 3)
        {
            healthBar.GetComponent<Image>().color = Color.red;

            // Met à jour l'intensité de l'effet de sang
            float bloodIntensity = 1f - (entity.life / entity.maxLife);
            SetBloodIntensity(bloodIntensity);
            Debug.Log("_BloodIntensity : " + bloodEffectMaterial.GetFloat("_BloodIntensity"));
        }
        else
        {
            healthBar.GetComponent<Image>().color = Color.green;
            
            SetBloodIntensity(0f);
            Debug.Log("_BloodIntensity : " + bloodEffectMaterial.GetFloat("_BloodIntensity"));
        }
    }

    private void SetBloodIntensity(float intensity)
    {
        bloodEffectMaterial.SetFloat("_BloodIntensity", intensity);
    }
}