using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardHearing : MonoBehaviour
{
    private GameManager gm;
    public Vector3 NoiseLocation { get { return noiseLocation; } }
    private Vector3 noiseLocation;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        Noise noiseObj = _other.GetComponent<Noise>();
        if (noiseObj != null)
        {
            if(noiseObj.MadeNoise == true)
            {
                noiseLocation = _other.gameObject.transform.position;
                GetComponentInParent<GuardBehaviour>().SearchNoise();
            }

        }
    }
}
