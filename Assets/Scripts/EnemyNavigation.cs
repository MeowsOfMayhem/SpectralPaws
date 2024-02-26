using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{

    private Animator anim;

    private NavMeshAgent navAgent;

    [SerializeField]
    private Transform[] destinationPoints;

    private Vector3 currentDestination;

    private int destinationIndex;

    private bool moveToPlayer;

    //private Collider _col;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
       // _col = GetComponent<Collider>();

        // get the current desination and make the agent
        // go towards that destination
        currentDestination = destinationPoints[Random.Range(0, destinationPoints.Length)].position;
        navAgent.SetDestination(currentDestination);
    }

    private void Update()
    {
        AnimatePlayer();
        CheckIfAgentReachedDestination();
    }

    void AnimatePlayer()
    {
        if (navAgent.velocity.magnitude > 0)
        {
            anim.SetBool("Walk", true);
        }
        else
            anim.SetBool("Walk", false);
    }

    void SetNewDestination()
    {
        while (true)
        {
            destinationIndex = Random.Range(0, destinationPoints.Length);

            if (currentDestination != destinationPoints[destinationIndex].position)
            {
                currentDestination = destinationPoints[destinationIndex].position;
                navAgent.SetDestination(currentDestination);
                break;
            }

        }
    }

    void CheckIfAgentReachedDestination()
    {
        if (!navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f)
                {
                    if (moveToPlayer)
                    {
                        Debug.Log("Attack the player");
                    }
                    else
                    {
                        SetNewDestination();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveToPlayer = true;
            navAgent.SetDestination(other.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            moveToPlayer = false;
            SetNewDestination();
        }
    }

} // class