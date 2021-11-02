using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField]
    private float noiseDuration = 1f;
    [SerializeField]
    private SphereCollider noiseRadius;
    private void Awake()
    {
        noiseRadius = GetComponentInChildren<SphereCollider>();
    }
    private void OnCollisionEnter(Collision _other)
    {
        StartCoroutine(MakeNoise());

    }

    private IEnumerator MakeNoise()
    {
        noiseRadius.enabled = true;
        yield return new WaitForSeconds(noiseDuration);
        noiseRadius.enabled = false;
    }
}
