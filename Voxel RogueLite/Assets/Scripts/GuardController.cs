using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{
    private bool seesPlayer = false;
    private GameObject player;
    private NavMeshAgent agent;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }
    private void OnCollisionEnter(Collision _other)
    {
        //if guard collides with player
        if(_other.collider.CompareTag("Player"))
        {
            seesPlayer = false;
            //Destroy(_other.gameObject); //destroy player 
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
                    seesPlayer = true; //guard sees player

                    Debug.DrawRay(transform.position, dir * hit.distance, Color.red);
                    Debug.Log("Hit");
                }
                else
                {
                    seesPlayer = false; //guard does not see player
                }
            }
            #endregion
        }
    }
    private void Update()
    {
        //if guard sees player
        if(seesPlayer)
        {
            MoveTowardsPlayer(); //move to him
        }
    }
    /// <summary>
    /// MoveTowardsPlayer makes the AI move towards the player
    /// </summary>
    private void MoveTowardsPlayer()
    {
        agent.SetDestination(player.transform.position); //move towards the player's position
    }
}
