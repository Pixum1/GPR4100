using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVision : MonoBehaviour
{
    GuardBehaviour gBehaviour; //the Behaviour component of the guard
    public bool SeesPlayer { get { return seesPlayer; } }
    private bool seesPlayer; //bool that determines wether the guard has clear sight on the player or not
    public Vector3 LastKnownPlayerPos { get { return lastKnownPlayerPos; } set { lastKnownPlayerPos = value; } }
    Vector3 lastKnownPlayerPos; //the coordinates where the guard saw the player last
    
    PlayerController player; //the player

    [SerializeField]
    private float visionRange; //the radius of the Spherecast

    [SerializeField]
    private LayerMask guardLayer; //the layer where guards are

    private void Awake()
    {
        gBehaviour = GetComponentInParent<GuardBehaviour>();
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        CheckForPlayer();

        CheckForGuard();
    }
    /// <summary>
    /// Function that updates the guards behaviour once the player enters his vision
    /// </summary>
    private void CheckForPlayer()
    {
        if(player != null)
        {
            //check if the player entered the guards vision range
            if (Vector3.Distance(transform.position, player.transform.position) <= visionRange)
            {
                if (RayHitTarget(player.gameObject, transform.position))
                {
                    seesPlayer = true; //guard sees player
                    gBehaviour.CurrentBehaviour = GuardBehaviour.EBehaviour.chasing; //change behaviour to chasing
                    lastKnownPlayerPos = player.transform.position; //update player location information
                    gBehaviour.Alarmed = false; //guard is no longer alarmed
                }
                else
                    seesPlayer = false;
            }
            else
                seesPlayer = false; //guard does not see player
        }
    }

    /// <summary>
    /// Function that collects all guards in the guards vision range and changes its behaviour according to other guards
    /// </summary>
    private void CheckForGuard()
    {
        Collider[] guards = Physics.OverlapSphere(transform.position, visionRange, guardLayer); //collect all guards within range in an array

        //if the array is not empty
        if(guards.Length != 0)
        {
            GameObject guard = guards[0].gameObject; //get first entry in array

            //if the guard sees the first entry in array
            if(RayHitTarget(guard, transform.position))
            {
                AdaptBehaviour(guard.GetComponent<GuardBehaviour>()); //adapt behaviour accordingly
            }
        }
    }
    /// <summary>
    /// Changes the guards behaviour if he sees a guard that chases the player
    /// </summary>
    /// <param name="_guard">The guard that entered vision</param>
    private void AdaptBehaviour(GuardBehaviour _guard)
    {
        if(_guard.CurrentBehaviour == GuardBehaviour.EBehaviour.chasing)
        {
            gBehaviour.CurrentBehaviour = _guard.GetComponent<GuardBehaviour>().CurrentBehaviour; //adapt behaviour of other guard
            lastKnownPlayerPos = _guard.GetComponentInChildren<GuardVision>().LastKnownPlayerPos; //get player position of other guard
        }
    }

    /// <summary>
    /// Returns true if the target was hit by the Ray
    /// </summary>
    /// <param name="_target">Target GameObject</param>
    /// <param name="_origin">Origin of the Ray</param>
    /// <returns></returns>
    private bool RayHitTarget(GameObject _target, Vector3 _origin)
    {
        Vector3 dir = _target.transform.position - _origin; //direction from origin to target

        if (Physics.Raycast(_origin, dir, out RaycastHit hit, Mathf.Infinity))
        {
            //if ray hit the targets gameobject collider
            if (hit.collider == _target.GetComponent<Collider>())
                return true;
            else
                return false;
        }
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}

