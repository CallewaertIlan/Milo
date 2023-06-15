using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float patrolSpeed = 6f;
    public float chaseSpeed = 10f;
    public float viewRange = 6f;
    public float attackRange = 2f;
    public int damageReceived = 0;
    private NavMeshAgent navAgent;
    private enum EnemyState
    {
        Patrol,
        Chase,
        Attack
    }
    private EnemyState currentState;
    private void Start()
    {
        currentState = EnemyState.Patrol;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = patrolSpeed;
        //navAgent.updateRotation = false; // Désactive la rotation automatique de l'ennemi par le NavMeshAgent
        SetRandomDestination();
    }
    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
            case EnemyState.Attack:
                Attack();
                break;
        }
    }
    private void Patrol()
    {

        // Vérifie si le joueur est visible
        if (Vector3.Distance(transform.position, player.position) <= viewRange) // See player
        {
            Debug.Log("Qui va là ?!");
            currentState = EnemyState.Chase; // Passe à l'état de poursuite
            navAgent.speed = chaseSpeed;
            return;
        }
        // Vérifie si l'ennemi est arrivé à destination
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            Debug.Log("Je patrouille");
            SetRandomDestination(); // Sélectionne une nouvelle destination de patrouille aléatoire
        }
    }
    private void Chase()
    {

        // Vérifie si le joueur est pas visible
        if (Vector3.Distance(transform.position, player.position) > viewRange) // Don't see
        {
            currentState = EnemyState.Patrol; // Passe à l'état de patrouille
            navAgent.speed = patrolSpeed;
            Debug.Log("Le joueur n'est plus visible, stop la bagarre, je repars patrouiller");
            return;
        }
        // Met à jour la destination du NavMeshAgent pour suivre le joueur
        navAgent.speed = chaseSpeed;
        navAgent.SetDestination(player.position);
        // Vérifie si l'ennemi est assez proche du joueur pour l'attaquer
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("Tu es assez proche, j'attaque !");
            currentState = EnemyState.Attack; // Passe à l'état d'attaque
            return;
        }
    }
    private void Attack()
    {

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("Attaque de la mort !");
            damageReceived += 1;
            Debug.Log("Degat reçu : " + damageReceived);
            return;
        }
        // Réalise l'attaque contre le joueur
        // Vérifie si le joueur n'est plus visible
        if (Vector3.Distance(transform.position, player.position) > viewRange) // Don't see
        {
            currentState = EnemyState.Patrol; // Passe à l'état de patrouille
            return;
        }
        if (Vector3.Distance(transform.position, player.position) <= viewRange)
        {
            Debug.Log("Le joueur veut s'échapper, je te lâche pas");
            currentState = EnemyState.Chase;
            return;
        }
    }
    private void SetRandomDestination()
    {
        // Sélectionne une destination de patrouille aléatoire sur le NavMesh
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position + randomPosition, out navHit, 10f, NavMesh.AllAreas);
        navAgent.SetDestination(navHit.position);
    }
}
