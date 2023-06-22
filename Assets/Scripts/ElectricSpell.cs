using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricSpell : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float range = 10.0f;

    private List<GameObject> enemies = new List<GameObject>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private IEnumerator ElectricSpellBounce(GameObject gameObject)
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, range);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy") && !enemies.Contains(collider.gameObject))
            {
                enemies.Add(collider.gameObject);
            }
        }

        while (enemies.Count > 0)
        {
            GameObject currentEnemy = enemies[0];
            enemies.RemoveAt(0);

            Vector3 direction = currentEnemy.transform.position - gameObject.transform.position;
            gameObject.GetComponent<Rigidbody>().velocity = direction.normalized * speed;

            yield return new WaitForSeconds(0.5f); // Attendre une seconde avant de passer à l'ennemi suivant
        }

        // Aucun ennemi à proximité, détruire la boule
        Destroy(gameObject);
    }

    private void LaunchElectricSpell()
    {
        GameObject gameObject = Instantiate(prefab, transform.position + transform.forward, Quaternion.identity);
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

        rigidbody.velocity = transform.forward * speed;
        rigidbody.useGravity = false;

        StartCoroutine(ElectricSpellBounce(gameObject));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LaunchElectricSpell();
        }
    }
}