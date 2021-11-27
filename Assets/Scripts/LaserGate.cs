using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGate : MonoBehaviour
{
    private GameManager gm;
    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            foreach (var guard in gm.Guards)
            {
                if (guard.gameObject != null)
                {
                    guard.AlarmGuards();
                }
            }
        }
    }
}
