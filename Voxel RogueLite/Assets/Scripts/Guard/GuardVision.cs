using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVision : MonoBehaviour
{
    GuardBehaviour gBehaviour;
    public bool SeesPlayer { get { return seesPlayer; } }
    private bool seesPlayer;
    public Vector3 LastKnownPlayerPos { get { return lastKnownPlayerPos; } set { lastKnownPlayerPos = value; } }
    Vector3 lastKnownPlayerPos; //the coordinates where the guard saw the player last
    private void Awake()
    {
        gBehaviour = GetComponentInParent<GuardBehaviour>();
    }
    private void OnTriggerStay(Collider _other)
    {
        PlayerController player = _other.GetComponent<PlayerController>();
        if (player != null)
        {
            RaycastHit hit;
            Vector3 dir = _other.transform.position - transform.position; //Vector from guard position to player position

            //cast ray towards player's position | collide with everything
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
            {
                //if ray collides with player's collider
                if (hit.collider.CompareTag("Player"))
                {
                    seesPlayer = true;
                    gBehaviour.CurrentBehaviour = GuardBehaviour.EBehaviour.chasing;
                    lastKnownPlayerPos = player.transform.position; //players position is saved
                    gBehaviour.Alarmed = false;
                }
                else
                {
                    seesPlayer = false;
                }
            }
        }
        if(_other.CompareTag("Guard"))
        {
            RaycastHit hit;
            Vector3 dir = _other.transform.position - transform.position;
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
            {
                if(hit.collider.CompareTag("Guard"))
                {
                    //if the other guard is chasing the player the guard will adapt to that behaviour
                    if (_other.GetComponent<GuardBehaviour>().CurrentBehaviour == GuardBehaviour.EBehaviour.chasing)
                    {
                        {
                            gBehaviour.CurrentBehaviour = _other.GetComponent<GuardBehaviour>().CurrentBehaviour;
                            lastKnownPlayerPos = _other.GetComponentInChildren<GuardVision>().LastKnownPlayerPos;
                        }
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider _other)
    {
        PlayerController player = _other.GetComponent<PlayerController>();
        if(player != null)
        {
            seesPlayer = false;
        }
    }
}

