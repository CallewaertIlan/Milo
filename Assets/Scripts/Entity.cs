using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public float life = 0;
    [SerializeField] public float maxLife;
    [SerializeField] protected float startMaxLife;

    protected float timeLastUseMana;
    protected float pourcentManaGainThisFrame;
    [SerializeField] public float mana = 0;
    [SerializeField] public float maxMana;
    [SerializeField] protected float startMaxMana;
    [SerializeField] protected float timeToGainMaxMana;
    [SerializeField] protected float timeBeforeStartGainMana;

    [SerializeField] protected float speed;

    [SerializeField] protected float damage;
    [SerializeField] protected float startDamage;

    // Start is called before the first frame update
    protected void Start()
    {
        AddLife(maxLife);
        timeLastUseMana = Time.time;

        startMaxLife = maxLife;
        startMaxMana = maxMana;
        startDamage = damage;
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

    public void AddLife(float hp)
    {
        // Ajoute de la vie et bloque aux hp max
        if (life + hp > maxLife) life = maxLife;
        else life += hp;
    }

    public void AddMana(float m)
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

    public void Hurt(float dps)
    {
        // Prendre des dégats
        life -= dps;
    }

    public void SetMaxLifeWithStartValue(float l)
    {
        maxLife = startMaxLife + l;
    }

    public void SetMaxManaWithStartValue(float m)
    {
        maxMana = startMaxMana + m;
    }

    public void SetDamageWithStartValue(float d)
    {
        damage = startDamage + d;
    }
    
    public void SetTimeLastManaUse(float t)
    {
        timeLastUseMana = t;
    }
}
