using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public bool MadeNoise { get { return madeNoise; } set { madeNoise = value; } }
    private bool madeNoise = false;

    public IEnumerator MakeNoise()
    {
        madeNoise = true;
        yield return new WaitForSeconds(1f);
        madeNoise = false;
    }
}
