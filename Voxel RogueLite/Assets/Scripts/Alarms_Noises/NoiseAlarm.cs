using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseAlarm : MonoBehaviour
{
    private Noise noiseObj;
    private void Awake()
    {
        noiseObj = GetComponent<Noise>();
    }
    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
            noiseObj.ActivateNoise();
    }
    private void OnTriggerExit(Collider _other)
    {
        if (_other.CompareTag("Player"))
            noiseObj.DeactivateNoise();
    }
}
