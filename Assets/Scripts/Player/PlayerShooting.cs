using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    private PlayerConfig playerConfig;
    private float shootingAngleDif = 20;
    private float timer;
    private Ray shootRay = new Ray();
    private int shootableMask;
    private ParticleSystem gunParticles;
    private List<LaserController> gunLines;
    private AudioSource gunAudio;
    private Light gunLight;
    private float effectsDisplayTime = 0.2f;
    [SerializeField] private GameObject laserPrefab;

    void Awake()
    {
        playerConfig = GetComponentInParent<PlayerConfig>();
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLines = new List<LaserController>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= playerConfig.timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= playerConfig.timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        for (int i = 0; i < gunLines.Count; i++)
        {
            gunLines[i].setEnable(false);
            Destroy(gunLines[i].gameObject);
        }
        gunLines.Clear();
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        gunLines.Clear();
        for (int i = 0; i < playerConfig.raysCount; i++)
        {
            var newLine = Instantiate(laserPrefab);
            gunLines.Add(newLine.GetComponent<LaserController>());
        }
        timer = 0f;
        gunAudio.Play();
        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();
        shootRay.origin = transform.position;

        bool isEven = (playerConfig.raysCount % 2 == 0);
        var temp = playerConfig.raysCount / 2 + (isEven ? 1 : 0);
        var initialDirection = transform.forward;
        if (isEven)
        {
            initialDirection = Quaternion
            .AngleAxis((float)(shootingAngleDif * 1.5), Vector3.up) * initialDirection;
        }
        var dests = new List<Vector3>();
        for (int i = 0 - temp; i <= (playerConfig.raysCount / 2); i++)
        {
            var direction = Quaternion.AngleAxis(shootingAngleDif * i, Vector3.up) * initialDirection;
            var dest = transform.position + direction * playerConfig.shootingRange;
            shootRay.direction = direction;
            if (Physics.Raycast(shootRay, out var shootHit, playerConfig.shootingRange, shootableMask))
            {
                EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
                enemyHealth?.TakeDamage(playerConfig.attackPower, shootHit.point);
                dest = shootHit.point;
            }
            dests.Add(dest);
        }
        for (int i = 0; i < playerConfig.raysCount; i++)
        {
            gunLines[i].setEnable(true);
            gunLines[i].AssignTarget(transform.position, dests[i]);
        }

    }
}