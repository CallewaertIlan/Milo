using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ManaBar : MonoBehaviour
{
    [SerializeField] Entity entity;
    [SerializeField] GameObject manaBar;
    [SerializeField] Text myMana;

    public Image manaBarImage;

    void Start()
    {
        manaBar.GetComponent<Image>().color = Color.blue;
    }

    void Update()
    {
        manaBarImage.fillAmount = entity.mana / entity.maxMana;
        myMana.text = entity.mana.ToString();
    }
}