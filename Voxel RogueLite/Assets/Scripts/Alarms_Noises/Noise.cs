using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField]
    private float noiseDuration = 1f;
    [SerializeField]
    private SphereCollider noiseRadius;
    private void OnCollisionEnter(Collision _other)
    {
        StartCoroutine(MakeNoise());
    }

    public IEnumerator MakeNoise()
    {
        noiseRadius.enabled = true;
        yield return new WaitForSeconds(noiseDuration);
        noiseRadius.enabled = false;
    }
    public void MakeNoiseOnce()
    {
        noiseRadius.enabled = true;
    }
}
