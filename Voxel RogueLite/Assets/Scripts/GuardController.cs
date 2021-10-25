using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    private GameObject player;

    private NavMeshAgent agent;
    private enum EBehaviour{ patrolling, searching, chasing}
    private int currBehaviour;

    private bool inSight = false;
    private bool inCombat = false;
    private Vector3 lastKnowPlayerPos;

    private List<Transform> patrolPoints = new List<Transform>();

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        #region Get Patrol Points and set Parent = null
        //go through every child in the guard Object
        foreach (Transform child in transform)
        {
            //if it's a patrol point
            if(child.CompareTag("PatrolPoint"))
            {
                patrolPoints.Add(child.transform); //add it to the list
            }
        }
        //go through every Transform on patrolPoints
        foreach(Transform point in patrolPoints)
        {
            point.SetParent(null); //set parent of point to null
        }
        #endregion
        //start guard patrol
    }

    private void OnCollisionEnter(Collision _other)
    {
        //if guard collides with player
        if(_other.collider.CompareTag("Player"))
        {
            ///seesPlayer = false;
            Destroy(_other.gameObject); //destroy player 
        }
    }
    private void OnTriggerStay(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            #region Raycast / Vision
            RaycastHit hit;
            Vector3 dir = _other.transform.position - transform.position; //Vector from guard position to player position

            //cast ray towards player's position | collide with everything
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
            {
                //if ray collides with player's collider
                if (hit.collider.CompareTag("Player"))
                {
                    inSight = true;
                    lastKnowPlayerPos = player.transform.position;

                    if (!inCombat)
                    {
                        StartCoroutine(BehaviourUpdate(EBehaviour.searching));
                        Debug.Log("Start Coroutine searching");
                    }
                }
            }
            #endregion
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            inSight = false;
        }
    }

    /// <summary>
    /// MoveTowardsPlayer makes the AI move towards the player
    /// </summary>
    private void MoveTowards(Vector3 _location)
    {
        agent.SetDestination(_location); //move towards the player's position
    }

    private void Patrol()
    {
        int p = Random.Range(0, patrolPoints.Count);
        if (!agent.hasPath || Vector3.Distance(transform.position, patrolPoints[p].position) <= .01f)
        {
            Debug.Log("Move");
            agent.SetDestination(patrolPoints[p].position);
        }
    }
    private void Update()
    {
        //Debug.Log($"playerLoc = {lastKnowPlayerPos}");
        if(inCombat)
        {
            StartCoroutine(BehaviourUpdate(EBehaviour.chasing));
        }
        if(!inSight && !inCombat)
        {
            StartCoroutine(BehaviourUpdate(EBehaviour.patrolling));
        }
    }

    private IEnumerator BehaviourUpdate(EBehaviour behaviour)
    {
        switch (behaviour)
        {
            case EBehaviour.patrolling:
                Patrol(); //start patrolling
                break;

            case EBehaviour.searching:
                agent.ResetPath();
                yield return new WaitForSeconds(1.5f); //wait 

                //if player stays in LOS until guard recognizes him
                if (inSight)
                {
                    inCombat = true;
                }
                //if player walks out of LOS before guard recognizes him
                else
                {
                    inCombat = false;
                }
                break;

            case EBehaviour.chasing:
                //if guard reached the last known player location and lost sight of him
                if(!inSight && Vector3.Distance(transform.position, lastKnowPlayerPos) <= 1f)
                {
                    agent.ResetPath(); //reset his path
                    yield return new WaitForSeconds(1.5f); //wait
                    inCombat = false; //guard leaves combat
                    StartCoroutine(BehaviourUpdate(EBehaviour.patrolling)); //start patrolling
                }
                //if guard still sees the player
                else if (inSight)
                {
                    //if he reached the player's position
                    if(Vector3.Distance(transform.position, lastKnowPlayerPos) <= 1f)
                    {
                        StartCoroutine(BehaviourUpdate(EBehaviour.chasing)); //check again
                    }
                    //if he's still far away from the position
                    else
                    {
                        MoveTowards(lastKnowPlayerPos); //move towards position
                    }
                }
                break;
        }
    }
}
