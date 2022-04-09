using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerConfig : MonoBehaviour
{
    const int maxAttackPower = 200;
    const int maxSpeed = 10;
    [Range(1, 1000)] public float maxHealth = 100;
    [Range(0, maxAttackPower)] public float attackPower = 20;

    [Range(1, 10)] public float moveSpeed = 6;
    [Range(1, 15)] public int raysCount = 1;
    [Range(1, 20)] public int shootingRange = 10;
    [Range(0.05f, 0.30f)] public float timeBetweenBullets = 0.15f;

    public int MaxAttackPower => maxAttackPower;
    public int MaxSpeed => maxSpeed;
    
    public Text powerDisplay;
    public Text speedDisplay;

    void Awake ()
    {
                powerDisplay.text = attackPower.ToString();
                speedDisplay.text = moveSpeed.ToString();
    }
    
    public void updatePowerDisplay()
    {
        powerDisplay.text = attackPower.ToString();
    }
    
    public void updateSpeedDisplay()
    {
        speedDisplay.text = moveSpeed.ToString();
    }
}
