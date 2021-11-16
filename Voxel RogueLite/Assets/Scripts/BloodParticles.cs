using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    private void Update()
    {
        if(particles.time >= particles.startLifetime)
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
    }
}
