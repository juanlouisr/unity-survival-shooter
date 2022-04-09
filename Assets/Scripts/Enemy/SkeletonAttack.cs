using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    // Start is called before the first frame updatepublic float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    public float attackRange = 10f;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;

    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0.2f;
    [SerializeField] float firingRateMin = 0.2f;
    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        anim = GetComponent<Animator>();
        // Mendapatkan Enemy health
        enemyHealth = GetComponent<EnemyHealth>();
    }


    void Attack()
    {
        // Taking damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth && enemyHealth.currentHealth > 0)
        {
            UpdateSkeletonRotation();
            Fire();
        }
        else
        {
            StopAllCoroutines();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.Play("Idle");
        }
    }

    void Fire()
    {
        var distance = Vector3.Distance(transform.position, player.transform.position);
        isFiring = (attackRange >= distance);
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    float GetRandomFiringRate()
    {
        float temp = Random.Range(firingRate - firingRateVariance,
                                    firingRate + firingRateVariance);
        return Mathf.Clamp(temp, firingRateMin, 5f);
    }

    IEnumerator FireContinously()
    {

        while (true)
        {
            Vector3 pos = transform.position + new Vector3(0, 0.35f, 0);

            GameObject projectile = Instantiate(projectilePrefab,
                        pos,
                        Quaternion.identity);
            projectile.GetComponent<SkeletonProjectile>().damage = attackDamage;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * projectileSpeed;

            Destroy(projectile, projectileLifetime);
            if (useAI)
            {
                yield return new WaitForSeconds(GetRandomFiringRate());
            }
            else
            {
                yield return new WaitForSeconds(firingRate);
            }

        }
    }

    void UpdateSkeletonRotation()
    {
        Vector3 lookVector = player.transform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }


}
