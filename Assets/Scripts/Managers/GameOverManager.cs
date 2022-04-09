using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text warningText;
    public PlayerHealth playerHealth;
    public GameOverScreen GameOverScreen;
    // public ScoreManager ScoreManager;
    // public float restartDelay = 5f;
    


    Animator anim;
    // float restartTimer;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            // Debug.Log(score);
            // anim.SetTrigger("GameOver");
            GameOverScreen.Setup(ScoreManager.score);
            // restartTimer += Time.deltaTime;

            // if (restartTimer >= restartDelay)
            // {
            //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // }
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        if (playerHealth.currentHealth > 0)
        {
            warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));
            anim.SetTrigger("Warning");

        }
    }
}