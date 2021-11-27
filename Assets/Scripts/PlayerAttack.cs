using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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
    public bool hasGun;

    private float rangeCooldown;
    [SerializeField]
    private float rangeAttacksPerSecond;
    [SerializeField]
    private Projectile projectilePrefab;
    [SerializeField]
    private float impulsePower;
    [SerializeField]
    private float lifeSpan;
    [SerializeField]
    private AudioSource launchSound;
    [SerializeField]
    private GameObject gunObj;

    private void Awake()
    {
        meleeCooldown = 1 / meleePerSecond;
        rangeCooldown = 1 / rangeAttacksPerSecond;
    }
    private void Update()
    {
        meleeCooldown -=Time.deltaTime;
        rangeCooldown -=Time.deltaTime;

        if(meleeCooldown < 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                MeleeAttack();
            }
        }
        if(rangeCooldown < 0)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        if (hasGun)
            gunObj.SetActive(true);
        else
            gunObj.SetActive(false);
    }

    private void MeleeAttack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, meleeRadius, enemyLayer);
        foreach(Collider enemy in hitEnemies)
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

        while(_knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector3 dir = -_obj.transform.forward;
            _obj.AddForce(dir * _knockbackPower);
        }
        yield return 0;
    }

    private void Shoot()
    {
        if(hasGun)
        {//if attack time is over 0
            if (rangeCooldown < 0)
            {
                Launch();
                rangeCooldown = 1.0f / rangeAttacksPerSecond; //update attack time
            }
        }
    }
    private void Launch()
    {
        Projectile go = Instantiate(projectilePrefab, transform.position, transform.parent.rotation); //instantiate projectile
        go.Launch(transform.forward * impulsePower, lifeSpan, gameObject); //launch projectile
        PlayLaunchSound();
    }
    private void PlayLaunchSound()
    {
        launchSound.pitch = 1f;
        float rndPitch = Random.Range(launchSound.pitch - .15f, launchSound.pitch + .15f);
        launchSound.pitch = rndPitch;
        launchSound.Play();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, meleeRadius);
    }
}
