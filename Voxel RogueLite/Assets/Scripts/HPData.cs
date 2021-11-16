using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPData : MonoBehaviour
{
    public float MaxHP { get { return m_maxHP; } }
    [SerializeField]
    private float m_maxHP; //maximum hp
    [SerializeField]
    private GameObject bloodParticles;
    float tempHP;

    [SerializeField]
    GameObject deadPrefab;
    [SerializeField]
    GameObject pistolPrefab;
    public float CurrentHP
    {
        get
        {
            return m_currentHP;
        }
        set
        {
            //only allow values above 0 and below maxHP to be set as current hp
            m_currentHP = Mathf.Clamp(value, 0, m_maxHP);
        }
    }
    private float m_currentHP; //current hp save

    private void Awake()
    {
        CurrentHP = m_maxHP;
        tempHP = m_currentHP;
    }
    private void OnCollisionEnter(Collision _other)
    {
        Projectile projectile = _other.gameObject.GetComponent<Projectile>();
        //if hit by projectile
        if (projectile != null)
        {
            //if projectile hits the gameobject that launched it
            if(projectile.ObjectThatShot.tag != gameObject.tag)
            {
                CurrentHP -= projectile.Damage; //subtract projectile damage
            }
        }
    }

    private void Update()
    {
        if(m_currentHP != tempHP)
        {
            Instantiate(bloodParticles, transform.position, Quaternion.identity);
            tempHP = m_currentHP;
        }
    }
    private void LateUpdate()
    {
        //if a object has 0 life and is not the player
        if(CurrentHP <= 0)
        {
            Instantiate(deadPrefab, transform.position, deadPrefab.transform.rotation);
            if(pistolPrefab != null)
                Instantiate(pistolPrefab, new Vector3(transform.position.x + 2f, pistolPrefab.transform.position.y, transform.position.z), pistolPrefab.transform.rotation);
            Destroy(gameObject);
        }
    }
}
