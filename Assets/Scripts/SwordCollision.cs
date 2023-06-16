using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    private bool isAttacking = false;

    public void StartAttack()
    {
        isAttacking = true;
    }

    public void EndAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttacking)
        {
            Destroy(GameObject.Find("Monster"));
        }
    }
}
