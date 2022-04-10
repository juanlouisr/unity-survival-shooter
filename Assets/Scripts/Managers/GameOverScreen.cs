using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text pointText;
        [SerializeField] bool useTime;


    public void Setup(int score){
        Time.timeScale = 0;
        gameObject.SetActive(true);
        if (useTime) 
        {
            pointText.text = score.ToString() + " s";
        }
        else
        {
            pointText.text = score.ToString() + " POINTS";
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }
    
}
