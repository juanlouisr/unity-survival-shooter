using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    public int shootingRays = 3;

    private float shootingAngleDif = 20;

    private float timer;
    private Ray shootRay = new Ray();
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private List<LaserController> gunLines;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = 0.2f;
    [SerializeField] private LaserController laserPrefab;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLines = new List<LaserController>();
        for (int i = 0; i < shootingRays; i++)
        {
            var newLine = Instantiate(laserPrefab);
            gunLines.Add(newLine);
        }
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        for (int i = 0; i < shootingRays; i++)
        {
            gunLines[i].setEnable(false);
        }
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        shootRay.origin = transform.position;
        bool isEven = (shootingRays % 2 == 0);
        var temp = shootingRays / 2 + (isEven ? 1 : 0);
        var initialDirection = transform.forward;
        if (isEven)
        {
            initialDirection = Quaternion
            .AngleAxis((float)(shootingAngleDif * 1.5), Vector3.up) * initialDirection;
        }
        var dests = new List<Vector3>();
        for (int i = 0 - temp; i <= (shootingRays / 2); i++)
        {
            var direction = Quaternion.AngleAxis(shootingAngleDif * i, Vector3.up) * initialDirection;
            var dest = transform.position + direction * range;
            shootRay.direction = direction;
            if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                enemyHealth?.TakeDamage(damagePerShot, shootHit.point);
                dest = shootHit.point;
            }
            dests.Add(dest);
        }
        for (int i = 0; i < shootingRays; i++)
        {
            gunLines[i].setEnable(true);
            gunLines[i].AssignTarget(transform.position, dests[i]);
        }

    }
}