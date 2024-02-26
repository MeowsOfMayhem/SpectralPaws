using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler1 : MonoBehaviour
{

    public Rigidbody theRB;
    public float moveSpeed;

    public bool shouldChasePlayer;
    public float rangeToChasePlayer;
    private Vector3 moveDirecton;

    public bool shouldRunAway;
    public float runawayRange;

    public bool shouldWonder;
    public float wanderLenght, pauseLenght;
    private float wanderCounter, pauseCounter;
    private Vector3 wanderDirection;

    public bool shouldPatrol;
    public Transform[] patrolPoints;
    private int currentPatrolPoint;

    // Start is called before the first frame update
    void Start()
    {
        if(shouldWonder)
        {
            pauseCounter = Random.Range(pauseLenght * .75f, pauseLenght * 1.25f);
        }
        if(shouldPatrol && patrolPoints.Length >0)
        {
            currentPatrolPoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // print(Vector3.Distance(transform.position, PlayerMovement2.instance.transform.position));
        //print(transform.position);
        //print(PlayerMovement2.instance.transform.position);
        //print(Vector3.Distance(transform.position, PlayerMovement2.instance.transform.position));
        //print(currentPatrolPoint);
        //print(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position));
       // print(PlayerMovement2.instance.transform.position);

        moveDirecton = Vector3.zero;

        if(Vector3.Distance(transform.position,PlayerMovement2.instance.transform.position) < rangeToChasePlayer && shouldChasePlayer)
        {
           // print("wlazlo!");
            moveDirecton = PlayerMovement2.instance.transform.position - transform.position;
           // print(moveDirecton);
        } else
        {
            if (shouldWonder)
            {
                if (wanderCounter > 0)
                {
                    wanderCounter -= Time.deltaTime;

                    // move the enemy

                    moveDirecton = wanderDirection;

                    if (wanderCounter <= 0)
                    {
                        pauseCounter = Random.Range(pauseLenght * .75f, pauseLenght * 1.25f);
                    }
                }

                if (pauseCounter > 0)
                {
                    pauseCounter -= Time.deltaTime;

                    if (pauseCounter <= 0)
                    {
                        wanderCounter = Random.Range(wanderLenght * .75f, wanderLenght * 1.25f);
                        wanderDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                    }
                }
            }
            if(shouldPatrol)
            {
                moveDirecton = patrolPoints[currentPatrolPoint].position - transform.position;
                print(Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position));

                if (Vector3.Distance(transform.position, patrolPoints[currentPatrolPoint].position) < 0.5f)
                    {
                    currentPatrolPoint++;
                    if(currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }
                }
            }
        }

        if(shouldRunAway && Vector3.Distance(transform.position, PlayerMovement2.instance.transform.position) < runawayRange)
        {
            moveDirecton = transform.position - PlayerMovement2.instance.transform.position;
        }

        //moveDirecton.y = 0;
        moveDirecton.Normalize();

        theRB.velocity = moveDirecton * moveSpeed;
    }
}
