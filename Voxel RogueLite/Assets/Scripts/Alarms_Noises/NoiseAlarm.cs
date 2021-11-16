using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseAlarm : MonoBehaviour
{
    private Noise noiseObj;
    private PlayerController player;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Material[] mats;
    [SerializeField]
    private LineRenderer line;

    private void Awake()
    {
        noiseObj = GetComponent<Noise>();
    }
    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if(player != null)
        {
            if (Vector3.Distance(transform.position, player.transform.position) <= radius)
            {
                Vector3 dir = player.transform.position - transform.position;
                if (Physics.Raycast(transform.position, dir, out RaycastHit hit, Mathf.Infinity))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        line.material = mats[1];
                        noiseObj.MadeNoise = true;
                    }
                }
            }
            else
            {
                line.material=mats[0];
                noiseObj.MadeNoise = false;
            }
        }
        else
            line.material=mats[0];
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
