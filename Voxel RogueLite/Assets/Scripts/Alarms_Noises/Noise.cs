using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField]
    private float noiseDuration = 1f;
    [SerializeField]
    private SphereCollider noiseRadius;

    public IEnumerator MakeNoise()
    {
        noiseRadius.enabled = true;
        yield return new WaitForSeconds(noiseDuration);
        noiseRadius.enabled = false;
    }
    public void ActivateNoise()
    {
        noiseRadius.enabled = true;
    }
    public void DeactivateNoise()
    {
        noiseRadius.enabled = false;
    }
}
