using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealthbar : MonoBehaviour
{
    private HPData hp;
    [SerializeField]
    private float fullLenght = 1.5f;
    private void Awake()
    {
        hp = GetComponentInParent<HPData>();
    }
    private void Update()
    {
        if (hp.CurrentHP != hp.MaxHP)
            GetComponent<MeshRenderer>().enabled = true;
        else
            GetComponent<MeshRenderer>().enabled = false;

        float healthbarPercent = (hp.MaxHP - hp.CurrentHP) / 100;
        gameObject.transform.localScale = new Vector3(fullLenght - (healthbarPercent * fullLenght), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
}
