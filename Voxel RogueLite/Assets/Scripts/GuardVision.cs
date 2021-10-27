using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Guard
{
    public class GuardVision : MonoBehaviour
    {
        public bool InSight { get { return inSight; } }
        private bool inSight; //bool that checks if player entered vision

        public Vector3 LastKnownPlayerPos { get { return lastKnownPlayerPos; } }
        Vector3 lastKnownPlayerPos; //the coordinates where the guard saw the player last

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
                        inSight = true; //player is in sight
                        lastKnownPlayerPos = player.transform.position; //players position is saved
                    }
                    else
                        inSight = false; //player is out of sight
                }
            }
        }
        private void OnTriggerExit(Collider _other)
        {
            PlayerController player = _other.GetComponent<PlayerController>();
            //if player exits vision
            if (player != null)
            {
                inSight = false; //player is out of sight
            }
        }
    }
}

