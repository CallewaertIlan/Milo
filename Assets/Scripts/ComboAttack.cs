using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour
{
    public int maxComboCount = 3;
    public float comboDelay = 1f;

    private int comboCount = 0;
    private float timeSinceLastAttack = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        timeSinceLastAttack += Time.deltaTime;
    }

    void Attack()
    {
        comboCount++; // Incrémenter le compteur du combo
        timeSinceLastAttack = 0f; // Réinitialiser le temps écoulé
        print("Attaque" + comboCount);
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
}