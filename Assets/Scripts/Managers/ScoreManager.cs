using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int killingScore;
    public static int actualScore;
    [SerializeField] bool useTime;

    Text text;

    Timer timer;


    void Awake ()
    {
        text = GetComponent<Text>();
        timer = GetComponent<Timer>();
        killingScore = 0;
        if (useTime) 
        {
            actualScore = timer.score;
        }
        else
        {
            actualScore = killingScore;
        }
    }


    void Update ()
    {
        if (useTime) 
        {
            actualScore = timer.score;
        }
        text.text = "Score: " + actualScore;
    }
}
