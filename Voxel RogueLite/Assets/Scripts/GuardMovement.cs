using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMovement : MonoBehaviour
{
    private GuardBehaviour gBehaviour;
    private GuardVision gVision;
    public NavMeshAgent Agent { get { return agent; } }
    public float MaxDistanceToPlayer { get { return maxDistanceToPlayer; } }
    private NavMeshAgent agent;
    private List<Transform> patrolPoints = new List<Transform>(); //list of all patrol points of the guard
    [SerializeField]
    [Tooltip("The distance at which the guard stops moving towards the player")]
    private float maxDistanceToPlayer; //the distance at which the guard stops moving towards the player
    private void Awake()
    {
        gBehaviour = GetComponent<GuardBehaviour>();
        agent = GetComponent<NavMeshAgent>();
        gVision = GetComponentInChildren<GuardVision>();

        #region Patrol Points
        //go through every child in the guard Object
        foreach (Transform child in transform)
        {
            //if it's a patrol point
            if (child.CompareTag("PatrolPoint"))
            {
                patrolPoints.Add(child.transform); //add it to the list
            }
        }
        //go through every Transform on patrolPoints
        foreach (Transform point in patrolPoints)
        {
            point.SetParent(null); //set parent of point to null
        }
        #endregion
    }
    private void Update()
    {
        CalculateAction();
        Debug.DrawLine(transform.position, agent.destination, Color.red); //Debug: Draw line to current destination
    }
    private void CalculateAction()
    {
        #region Guard saw player and reached its destination
        if (gVision.LastKnownPlayerPos != null && Vector3.Distance(transform.position, gVision.LastKnownPlayerPos) <= 1)
        {
            gBehaviour.CurrentBehaviour = GuardBehaviour.EBehaviour.patrolling; //set current behaviour to patrolling
            //if this guard was sent to the location because of an alarm
            if(gBehaviour.Alarmed)
                GuardClearedAlarm(); //clear alarm for all guards
        }
        #endregion

        #region Guard is chasing and has a valid path
        if (gBehaviour.CurrentBehaviour == GuardBehaviour.EBehaviour.chasing)
        {
            if(PathIsValid(gVision.LastKnownPlayerPos)) //if the guard can reach the location
                MoveTowardsPlayer(gVision.LastKnownPlayerPos); //move to location
            
            else //if not
                gBehaviour.CurrentBehaviour = GuardBehaviour.EBehaviour.patrolling; //return to patrolling
        }
        #endregion

        #region Guard is patrolling
        if (gBehaviour.CurrentBehaviour == GuardBehaviour.EBehaviour.patrolling)
        {
            Patrol();
        }
        #endregion
    }
    /// <summary>
    /// This function sends all guards back to their patroling path once one guard reached the location of the alarm
    /// </summary>
    private void GuardClearedAlarm()
    {
        GameManager gm = FindObjectOfType<GameManager>();
        foreach (var guard in gm.Guards)
        {
            if (guard.gameObject != null)
            {
                GuardMovement move = guard.gameObject.GetComponent<GuardMovement>();
                guard.CurrentBehaviour = GuardBehaviour.EBehaviour.patrolling; //set current behaviour to patroling
                move.agent.ResetPath(); //reset path so guard can patrol
                guard.Alarmed = false; //guard is no longer alarmed
            }
        }
    }
    private bool PathIsValid(Vector3 _targetLocation)
    {
        NavMeshPath path = new NavMeshPath();

        //if guard can reach its destination
        if (agent.CalculatePath(_targetLocation, path) && path.status == NavMeshPathStatus.PathComplete)
            return true;
        
        else
            return false;
    }
    private void MoveTowardsPlayer(Vector3 _location)
    {
        agent.SetDestination(_location); //set ai destination to location
                                         //if distance to the players location is greater than 3
        if (Vector3.Distance(transform.position, gVision.LastKnownPlayerPos) >= maxDistanceToPlayer)
        {
            agent.isStopped = false; //start to move
        }
        if (Vector3.Distance(transform.position, gVision.LastKnownPlayerPos) <= maxDistanceToPlayer && gVision.SeesPlayer)
        {
            transform.LookAt(gVision.LastKnownPlayerPos); //rotate to the player
            agent.isStopped = true; //stop movement
        }
        if (Vector3.Distance(transform.position, gVision.LastKnownPlayerPos) <= maxDistanceToPlayer && !gVision.SeesPlayer)
        {
            agent.isStopped = false;
        }
    }
    private void Patrol()
    {
        //if agent has no path (so that the next patrol point will only be chosen when the guard reached the current destination)
        if (!agent.hasPath)
        {
            //search random patrolpoint and set destination to it's position
            int p = Random.Range(0, patrolPoints.Count);
            agent.SetDestination(patrolPoints[p].position);
            agent.isStopped = false; //start movement
        }
    }
}

