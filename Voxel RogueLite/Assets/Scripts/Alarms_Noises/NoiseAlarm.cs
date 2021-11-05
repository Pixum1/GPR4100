using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseAlarm : MonoBehaviour
{
    private Noise noiseObj;
    private PlayerController player;
    [SerializeField]
    private float radius;
    private void Awake()
    {
        noiseObj = GetComponent<Noise>();
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= radius)
        {
            Vector3 dir = player.transform.position - transform.position;
            if(Physics.Raycast(transform.position, dir, out RaycastHit hit, Mathf.Infinity))
            {
                if(hit.collider.CompareTag("Player"))
                {
                    noiseObj.MadeNoise = true;
                }
            }
        }
        else
            noiseObj.MadeNoise = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
