using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolItem : MonoBehaviour
{
    PlayerAttack player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            PickUp();
        }
    }
    private void PickUp()
    {
        player.hasGun = true;
        Destroy(gameObject);
    }
}
