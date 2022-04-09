using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPower : MonoBehaviour
{
    public float currentPower;
    public Text powerDisplay;
    private PlayerConfig playerConfig;
    
    void Awake()
    {
        currentPower = GetComponentInChildren<PlayerConfig>().attackPower;
    }
    
    void start ()
    {

    }

    void update ()
    {
        powerDisplay.text = currentPower.ToString();
    }

    public void updatePowerDisplay()
    {
        powerDisplay.text = currentPower.ToString();
    }
}
