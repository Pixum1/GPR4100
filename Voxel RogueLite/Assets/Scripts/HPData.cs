using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPData : MonoBehaviour
{
    [SerializeField]
    private float m_maxHP; //maximum hp
    private float m_currentHP; //current hp save
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

    private void Awake()
    {
        CurrentHP = m_maxHP;
    }
    private void OnTriggerEnter(Collider _other)
    {
        Projectile projectile = _other.GetComponent<Projectile>();
        //if hit by projectile
        if (projectile != null)
        {
            //if projectile hits the gameobject that launched it
            if(projectile.ObjectThatShot.tag != gameObject.tag)
            {
                Debug.Log($"Hit {this.gameObject.name}");
                CurrentHP -= projectile.Damage; //subtract projectile damage
            }
        }
    }
}
