using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GuardHearing : MonoBehaviour
{
    public Vector3 NoiseLocation { get { return noiseLocation; } }
    private Vector3 noiseLocation;
    [SerializeField]
    private float noiseRadius;
    private List<Noise> noiseObjects = new List<Noise>();

    [SerializeField]
    private LayerMask noiseLayer;

    Noise noiseObj;

    private void Update()
    {
        UpdateNoiseObjects();

        MoveToNearestNoise();
    }

    /// <summary>
    /// Get all noise objects in the guards hearing radius
    /// </summary>
    private void UpdateNoiseObjects()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, noiseRadius, noiseLayer); //get all noise colliders inside the overlapsphere

        noiseObjects.Clear(); //clear the noiseObjects list

        //add each noise collider to the noiseobjects list
        foreach (Collider c in cols)
        {
            noiseObj = c.GetComponent<Noise>();
            noiseObjects.Add(noiseObj);
        }
    }

    /// <summary>
    /// Guard moves to the nearest noise
    /// </summary>
    private void MoveToNearestNoise()
    {
        if (noiseObjects.Count != 0)
        {
            //Sort the Locations based on their distance to the guard (nearest is first)
            noiseObjects = noiseObjects.OrderBy(obj => Vector3.Distance(this.transform.position, obj.transform.position)).ToList();
            if (noiseObj.MadeNoise)
            {
                //noiseLocation = noiseObjects[0].transform.position; //get noise location
                noiseLocation = noiseObjects[0].initPos;
                GetComponentInParent<GuardBehaviour>().SearchNoise(); //change behaviour
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, noiseRadius);
    }
}

