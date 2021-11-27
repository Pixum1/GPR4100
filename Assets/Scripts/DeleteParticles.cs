using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticles : MonoBehaviour {

    ParticleSystem particles;
    [SerializeField]
    private AudioSource collectSound;

    private void Awake() {
        particles = GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            collectSound.Play();
            //if (particles.time <= 0f) {
            //    Destroy(gameObject);
            //}
        }
    }
}
