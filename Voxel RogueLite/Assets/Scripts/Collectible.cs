using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField]
    private float m_Value;
    private GameManager m_Gm;
    [SerializeField]
    ParticleSystem particles;

    private void Awake()
    {
        m_Gm = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        m_Gm.AddCollectible(this);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.CompareTag("Player"))
        {
            particles.transform.parent = null;
            particles.Play();
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        m_Gm.RemoveCollectible(this);
    }
}
