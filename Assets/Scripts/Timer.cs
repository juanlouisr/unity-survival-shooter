using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;

    public void Start()
    {
        StartCoroutine(time());
    }
    
    IEnumerator time()
    {
        while (true)
        {
            score += 1;
            yield return new WaitForSeconds(1);
        }
    }
}
