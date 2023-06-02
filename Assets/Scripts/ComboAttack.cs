using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public int maxComboCount = 3;
    public float comboDelay = 1f;
    public float attackRate = 5f;
    public bool canAttack = true; 
    private int comboCount = 0;
    private float timeSinceLastAttack = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
        timeSinceLastAttack += Time.deltaTime;
    }

    void Attack()
    {
        // POUR CANCEL L'ATTAQUE SI ON ESQUIVE PAR EXEMPLE
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Debug.Log("Sortie de fonction");
        //    return;
        //}
        comboCount++; // Incrémenter le compteur du combo
        print("Attaque" + comboCount);
        timeSinceLastAttack = 0f; // Réinitialiser le temps écoulé
        if (comboCount >= 1)
        {
            StartCoroutine(AttackRate());
        }
        if (comboCount >= maxComboCount)
        {
            Debug.Log("Combo réussi !");
            comboCount = 0; // Réinitialiser le compteur du combo
            return;
        }
        StartCoroutine(ResetCombo());
    }

    IEnumerator ResetCombo()
    {
        yield return new WaitForSeconds(comboDelay);

        if (timeSinceLastAttack > comboDelay)
        {
            comboCount = 0;
            Debug.Log("Combo réinitialisé !");
        }
    }

    IEnumerator AttackRate()
    {
        Debug.Log("Tu ne peux pas attaquer !");
        canAttack = false;
        yield return new WaitForSeconds(attackRate);
        Debug.Log("Tu peux attaquer !");
        canAttack = true;
    }
}