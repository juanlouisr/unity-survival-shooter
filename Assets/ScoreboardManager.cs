using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score 
{
    public string name;
    public int score;
    public int wave;
    public string type = "wave";

    public Score(string name, int score, string type, int wave = 0)
    {
        this.name = name;
        this.score = score;
        this.wave = wave;
        this.type = type;
    }
}

public class ScoreboardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Score> scores;
    void Awake()
    {
        scores = new List<Score>();
    }

}
