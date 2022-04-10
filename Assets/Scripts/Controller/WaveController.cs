using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameOverScreen gameOverScreen;

    [SerializeField] Text waveText;

    Animator anim;

    public GameObject[] waves;
    bool _isFinishedSpawning = false;
    bool FinishiedSpawning { get => _isFinishedSpawning; }

    int currWaveIdx = 0;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        waveText.text = waves[currWaveIdx].name;
    }


    void Update()
    {
        if (currWaveIdx >= waves.Length)
        {
            _isFinishedSpawning = true;
            gameOverScreen.Setup(ScoreManager.actualScore);
            return;
        }

        var enemyManagers = waves[currWaveIdx].GetComponents<EnemyManager>();

        foreach (var enemyManager in enemyManagers)
        {
            enemyManager.enabled = true;
        }

        foreach (var gameManager in enemyManagers)
        {
            if (!gameManager.FinishiedSpawning)
            {
                return;
            }
        }
        currWaveIdx += 1;
        waveText.text = waves[currWaveIdx].name;
        anim.SetTrigger("Warning");

    }



}
