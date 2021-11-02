using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private float m_Value;
    private GameManager m_Gm;

    private void Awake()
    {
        m_Gm = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        m_Gm.AddCoin(this);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        m_Gm.RemoveCoin(this);
    }
}
