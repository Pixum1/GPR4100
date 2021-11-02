using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter(Collision _other)
    {
        if(_other.gameObject.CompareTag("Guard"))
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
