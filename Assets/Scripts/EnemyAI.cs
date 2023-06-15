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
        //navAgent.updateRotation = false; // D�sactive la rotation automatique de l'ennemi par le NavMeshAgent
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

        // V�rifie si le joueur est visible
        if (Vector3.Distance(transform.position, player.position) <= viewRange) // See player
        {
            Debug.Log("Qui va l� ?!");
            currentState = EnemyState.Chase; // Passe � l'�tat de poursuite
            navAgent.speed = chaseSpeed;
            return;
        }
        // V�rifie si l'ennemi est arriv� � destination
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            Debug.Log("Je patrouille");
            SetRandomDestination(); // S�lectionne une nouvelle destination de patrouille al�atoire
        }
    }
    private void Chase()
    {

        // V�rifie si le joueur est pas visible
        if (Vector3.Distance(transform.position, player.position) > viewRange) // Don't see
        {
            currentState = EnemyState.Patrol; // Passe � l'�tat de patrouille
            navAgent.speed = patrolSpeed;
            Debug.Log("Le joueur n'est plus visible, stop la bagarre, je repars patrouiller");
            return;
        }
        // Met � jour la destination du NavMeshAgent pour suivre le joueur
        navAgent.speed = chaseSpeed;
        navAgent.SetDestination(player.position);
        // V�rifie si l'ennemi est assez proche du joueur pour l'attaquer
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("Tu es assez proche, j'attaque !");
            currentState = EnemyState.Attack; // Passe � l'�tat d'attaque
            return;
        }
    }
    private void Attack()
    {

        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            Debug.Log("Attaque de la mort !");
            damageReceived += 1;
            Debug.Log("Degat re�u : " + damageReceived);
            return;
        }
        // R�alise l'attaque contre le joueur
        // V�rifie si le joueur n'est plus visible
        if (Vector3.Distance(transform.position, player.position) > viewRange) // Don't see
        {
            currentState = EnemyState.Patrol; // Passe � l'�tat de patrouille
            return;
        }
        if (Vector3.Distance(transform.position, player.position) <= viewRange)
        {
            Debug.Log("Le joueur veut s'�chapper, je te l�che pas");
            currentState = EnemyState.Chase;
            return;
        }
    }
    private void SetRandomDestination()
    {
        // S�lectionne une destination de patrouille al�atoire sur le NavMesh
        Vector3 randomPosition = Random.insideUnitSphere * 10f;
        NavMeshHit navHit;
        NavMesh.SamplePosition(transform.position + randomPosition, out navHit, 10f, NavMesh.AllAreas);
        navAgent.SetDestination(navHit.position);
    }
}
