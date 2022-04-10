using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {Zombunny, ZomBear, Hellephant, Skeleton, Bomber, Boss}

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemyType spawnEnemy;
    [SerializeField] float spawnTime = 3f;
    [SerializeField] int enemyCount = 1;

    [SerializeField] bool infiniteEnemies = true;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] public MonoBehaviour factory;
    IFactory Factory { get { return factory as IFactory; } }

    bool _isFinishedSpawning = false;

    public bool FinishiedSpawning {get => _isFinishedSpawning;}

    int spawnedEnemyCount = 0;

    PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }


    void Start ()
    {
        //Mengeksekusi fungs Spawn setiap beberapa detik sesui dengan nilai spawnTime
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {
        if (playerHealth.currentHealth <= 0f || (!infiniteEnemies && spawnedEnemyCount >= enemyCount))
        {
            _isFinishedSpawning = true;
            return;
        }

        spawnedEnemyCount += 1;

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        // Menduplikasi enemy
        Instantiate(Factory.FactoryMethod((int)spawnEnemy), spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
