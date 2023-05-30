using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected float life = 0;
    [SerializeField] protected float maxLife;

    protected float timeLastUseMana;
    protected float pourcentManaGainThisFrame;
    [SerializeField] protected float mana = 0;
    [SerializeField] protected float maxMana;
    [SerializeField] protected float timeToGainMaxMana;
    [SerializeField] protected float timeBeforeStartGainMana;

    [SerializeField] protected float speed;

    [SerializeField] protected float damage;

    // Start is called before the first frame update
    protected void Start()
    {
        AddLife(maxLife);
        timeLastUseMana = Time.time;
    }
    
    // Update is called once per frame
    protected void Update()
    {
        // Vérifie si l'entity est encore en vie sinon la fait mourir
        if (IsDead()) Death();

        // Vérifie si le temps avant de gagner du mana est passé, si oui gagne du mana 
        if (Time.time > timeLastUseMana + timeBeforeStartGainMana) GainMana();

        if (Input.GetKeyDown(KeyCode.P)) Hurt(10);
    }

    protected bool IsDead()
    {
        // renvoie si l'entity est en vie ou non
        if (life > 0) return false;
        return true;
    }

    protected void Death()
    {
        // fonction de mort de l'entity
        Destroy(gameObject);
    }

    protected void AddLife(float hp)
    {
        // Ajoute de la vie et bloque aux hp max
        if (life + hp > maxLife) life = maxLife;
        else life += hp;
    }

    protected void AddMana(float m)
    {
        // Ajoute de la mana et bloque aux max mana
        if (mana + m > maxMana) mana = maxMana;
        else mana += m;
    }

    protected void GainMana()
    {
        // Gagne du mana en fonction du mana max et du temps pour en gagner
        pourcentManaGainThisFrame = Time.deltaTime * 100 / timeToGainMaxMana;
        AddMana(pourcentManaGainThisFrame / 100 * maxMana);
    }

    protected void Hurt(float dps)
    {
        // Prendre des dégats
        life -= dps;
    }
}
