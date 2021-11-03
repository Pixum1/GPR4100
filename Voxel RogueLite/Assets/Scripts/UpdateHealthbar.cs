using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateHealthbar : MonoBehaviour
{
    private HPData hp;
    [SerializeField]
    private int fullLenght = 3;
    float lastHP;
    private void Awake()
    {
        hp = GetComponentInParent<HPData>();
    }
    private void Update()
    {
        StartCoroutine(ShowHealthbar());
        float healthbarPercent = (hp.MaxHP - hp.CurrentHP) / 100;
        gameObject.transform.localScale = new Vector3(fullLenght - (healthbarPercent * fullLenght), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }
    private IEnumerator ShowHealthbar()
    {
        if(lastHP != hp.CurrentHP)
        {
            GetComponent<MeshRenderer>().enabled = true;
            lastHP = hp.CurrentHP;
            yield return new WaitForSeconds(2f);
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
