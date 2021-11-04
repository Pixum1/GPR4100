using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnduranceBar : MonoBehaviour
{
    private Endurance endur;
    [SerializeField]
    private float fullLenght = 1.5f;
    float lastEndur;
    private void Awake()
    {
        endur = GetComponentInParent<Endurance>();
    }
    private void Update()
    {
        if (endur.GetEndurance < endur.MaxEndurance)
            GetComponent<MeshRenderer>().enabled = true;
        else if(endur.GetEndurance >= endur.MaxEndurance)
            GetComponent<MeshRenderer>().enabled = false;

        float endurbarPercent = (endur.GetEndurance / endur.MaxEndurance);
        gameObject.transform.localScale = new Vector3(endurbarPercent * fullLenght, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}
