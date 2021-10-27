using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Guard
{
    public class GuardMovement : MonoBehaviour
    {
        public NavMeshAgent Agent { get { return agent; } }
        public float MaxDistanceToPlayer { get { return maxDistanceToPlayer; } }
        private NavMeshAgent agent;
        private List<Transform> patrolPoints = new List<Transform>(); //list of all patrol points of the guard
        private GuardVision gVision;
        [SerializeField]
        [Tooltip("The distance at which the guard stops moving towards the player")]
        private float maxDistanceToPlayer; //the distance at which the guard stops moving towards the player
        private void Awake()
        {
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
            //on Start when guard has no path he patrols
            if (!agent.hasPath)
                Patrol();


            //if guard sees player
            if (gVision.InSight)
            {
                MoveTowardsPlayer(gVision.LastKnownPlayerPos);
            }
            //else if guard does not see player, he knows where the player was last at, and walked to that point
            else if (!gVision.InSight && gVision.LastKnownPlayerPos != null&& Vector3.Distance(transform.position, gVision.LastKnownPlayerPos) <= 1)
            {
                Patrol(); //patrol
            }
            //else (the guard did not walk to the last known location yet)
            else
            {
                agent.isStopped = false; //sstart to move again
            }
            Debug.DrawLine(transform.position, agent.destination, Color.red); //Debug: Draw line to current destination
        }
        private void MoveTowardsPlayer(Vector3 _location)
        {
            agent.SetDestination(_location); //set ai destination to location
            //if distance to the players location is greater than 3
            if (Vector3.Distance(transform.position, gVision.LastKnownPlayerPos) >= maxDistanceToPlayer)
            {
                agent.isStopped = false; //start to move
            }
            else
            {
                transform.LookAt(gVision.LastKnownPlayerPos); //rotate to the player
                agent.isStopped = true; //stop movement
            }
        }
        private void Patrol()
        {
            //if agent has no path (so that the next patrol point will only be chosen when the guard reached the current destination)
            if(!agent.hasPath)
            {
                //search random patrolpoint and set destination to it's position
                int p = Random.Range(0, patrolPoints.Count);
                agent.SetDestination(patrolPoints[p].position);
                agent.isStopped = false; //start movement
            }
        }
    }
}

