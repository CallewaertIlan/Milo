using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] GameObject healthBar;
    [SerializeField] Text myHealth;

    public Image healthBarImage;

    private void Start()
    {
        healthBar.GetComponent<Image>().color = Color.green;
    }

    void Update()
    {
        myHealth.text = entity.life.ToString();

        healthBarImage.fillAmount = entity.life / entity.maxLife;

        if (entity.life <= entity.maxLife / 2 && entity.life >= entity.maxLife / 3)
        {
            healthBar.GetComponent<Image>().color = Color.yellow;
        }

        else if (entity.life < entity.maxLife / 3)
        {
            healthBar.GetComponent<Image>().color = Color.red;
        }

        else
        {
            healthBar.GetComponent<Image>().color = Color.green;
        }
    }
}