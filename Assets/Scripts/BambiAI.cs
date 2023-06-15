using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BambiAI : MonoBehaviour
{
    public Transform player;
    public float patrolSpeed = 6f;
    public float fleeSpeed = 10f;
    public float fleeRange = 4f;
    private NavMeshAgent navAgent;
    private enum BambiState
    {
        Patrol,
        Flee
    }
    private BambiState currentState;
    private void Start()
    {
        currentState = BambiState.Patrol;
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = patrolSpeed;
        //navAgent.updateRotation = false; // Désactive la rotation automatique de l'ennemi par le NavMeshAgent
        SetRandomDestination();
    }
    private void Update()
    {
        switch (currentState)
        {
            case BambiState.Patrol:
                Patrol();
                break;
            case BambiState.Flee:
                Flee();
                break;
        }
    }
    private void Patrol()
    {
        // Vérifie si l'ennemi est assez proche du joueur pour m'enfuir
        if (Vector3.Distance(transform.position, player.position) <= fleeRange)
        {
            Debug.Log("Je m'enfuis, au secours !");
            currentState = BambiState.Flee; // Passe à l'état de fuite
            navAgent.speed = fleeSpeed;
            return;
        }
        // Vérifie si l'ennemi est arrivé à destination
        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            Debug.Log("Je patrouille");
            SetRandomDestination(); // Sélectionne une nouvelle destination de patrouille aléatoire
        }
    }
    private void Flee()
    {
        // Vérifie si l'ennemi est suffisamment éloigné du joueur pour revenir à l'état de patrouille
        if (Vector3.Distance(transform.position, player.position) > fleeRange)
        {
            Debug.Log("Ouf j'ai réussis à m'enfuir, mtn je mange la glace");
            currentState = BambiState.Patrol; // Passe à l'état de patrouille
        }
        // Met à jour la destination du NavMeshAgent pour fuir dans la direction opposée au joueur
        Vector3 fleeDirection = (transform.position - player.position).normalized;
        navAgent.SetDestination(transform.position + fleeDirection * 20f);
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
