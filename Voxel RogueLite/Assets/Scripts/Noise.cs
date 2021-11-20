using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public bool MadeNoise { get { return madeNoise; } set { madeNoise = value; } }
    private bool madeNoise = false;
    public Vector3 initPos;

    public IEnumerator MakeNoise()
    {
        madeNoise = true;
        initPos = transform.position;
        yield return new WaitForSeconds(1f);
        madeNoise = false;
    }
}
