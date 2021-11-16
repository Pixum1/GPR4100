using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAttack : MonoBehaviour
{
    [Header("Projectile Information")]
    [SerializeField]
    private Projectile m_projectilePrefab; //the projectile prefab
    [SerializeField]
    private Transform m_projectileSpawn; //the spawn location of the projectile
    [SerializeField]
    private float m_impulsePower = 20f; //the impulse at which the porjectile should be launched

    [Header("Projectile Stats")]
    [SerializeField]
    private float m_lifeSpan = 1f; //the time until the projectile gets destroyed
    [SerializeField]
    private float m_attacksPerSecond = 5f; //the attacks per second the guard should do
    private float m_attackTimeLeft;

    [Header("Guard Information")]
    [SerializeField]
    [Tooltip("Range at which the guard should start shooting at the player based on the distance he should keep between him and the player")]
    private float m_minShootRange;
    [SerializeField]
    private AudioSource m_launchSound;

    private GuardBehaviour gBehaviour; //vision component
    private GuardMovement gMovement; //movement compon
    private GuardVision gVision;

    public bool hasGun;
    private void Awake()
    {
        meleeCooldown = 1 / meleePerSecond;
        gVision = GetComponentInChildren<GuardVision>();
        gBehaviour = GetComponent<GuardBehaviour>();
        gMovement = GetComponent<GuardMovement>();
        m_attackTimeLeft = 1 / m_attacksPerSecond;
    }
    private void Update()
    {
        meleeCooldown -= Time.deltaTime;
        m_attackTimeLeft -= Time.deltaTime; //subtract attack time
        //if player is in sight
        if (gBehaviour.CurrentBehaviour == GuardBehaviour.EBehaviour.chasing && gVision.SeesPlayer)
        {
            if(hasGun)
            {
                //if guard reached his minimal shoot range distance to the player
                if (Vector3.Distance(gMovement.Agent.transform.position, gMovement.Agent.destination) <= gMovement.MaxDistanceToPlayer + m_minShootRange)
                {
                    Shoot(); //attack
                }
            }
            else
            {
                if(Vector3.Distance(gMovement.Agent.transform.position, gMovement.Agent.destination) <= 1)
                {
                    if(meleeCooldown < 0)
                        MeleeAttack();
                }
            }    
        }
    }
    [SerializeField]
    private float meleeRadius;
    [SerializeField]
    private float meleeDamage;
    public float meleeCooldown;
    [SerializeField]
    private float meleePerSecond;
    [SerializeField]
    private float knockbackStrength;
    [SerializeField]
    private float knockbackDuration;
    [SerializeField]
    private LayerMask enemyLayer;
    [SerializeField]
    private AudioSource punchSound;
    private void MeleeAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, meleeRadius, enemyLayer);
        foreach (Collider enemy in hitEnemies)
        {
            enemy.GetComponent<HPData>().CurrentHP -= meleeDamage;
            StartCoroutine(Knockback(knockbackDuration, knockbackStrength, enemy.GetComponent<Rigidbody>()));
        }
        punchSound.Play();
        meleeCooldown = 1 / meleePerSecond;
    }

    private IEnumerator Knockback(float _knockbackDuration, float _knockbackPower, Rigidbody _obj)
    {
        float timer = 0;

        while (_knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector3 dir = -_obj.transform.forward;
            _obj.AddForce(dir * _knockbackPower);
        }
        yield return 0;
    }

    private void Shoot()
    {
        //if attack time is over 0
        if (m_attackTimeLeft < 0)
        {
            Launch();

            m_attackTimeLeft = 1.0f / m_attacksPerSecond; //update attack time
        }
    }
    private void Launch()
    {
        Projectile go = Instantiate(m_projectilePrefab, m_projectileSpawn.position, m_projectileSpawn.rotation); //instantiate projectile
        go.Launch(transform.forward * m_impulsePower, m_lifeSpan, gameObject); //launch projectile
        PlayLaunchSound();
    }
    private void PlayLaunchSound()
    {
        m_launchSound.pitch = 1f;
        float rndPitch = Random.Range(m_launchSound.pitch - .15f, m_launchSound.pitch + .15f);
        m_launchSound.pitch = rndPitch;
        m_launchSound.Play();
    }
}

