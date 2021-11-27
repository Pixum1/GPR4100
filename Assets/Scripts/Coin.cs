using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Noise noiseObj;
    private void Awake()
    {
        noiseObj = GetComponent<Noise>();
    }
    private void OnCollisionEnter(Collision _other)
    {
        noiseObj.StartCoroutine(noiseObj.MakeNoise());

        if (_other.gameObject.CompareTag("Guard"))
            Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine(DestroyCoin(5));
    }
    private IEnumerator DestroyCoin(float _time)
    {
        yield return new WaitForSeconds(_time);
        Destroy(this.gameObject);
    }
}
