using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public static int actualScore;
    [SerializeField] bool useTime;

    Text text;

    Timer timer;


    void Awake ()
    {
        text = GetComponent<Text>();
        timer = GetComponent<Timer>();
        score = 0;
        if (useTime) 
        {
            actualScore = timer.score;
        }
        else
        {
            actualScore = score;
        }
    }


    void Update ()
    {
        if (useTime) 
        {
            text.text = "Time: " + timer.score;
        }
        else
        {
            text.text = "Score: " + score;
        }
    }
}
