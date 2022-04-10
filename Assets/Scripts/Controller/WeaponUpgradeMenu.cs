using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeMenu : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerConfig playerConfig;

    void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerConfig = player.GetComponent<PlayerConfig>();
        Debug.Log(playerConfig);
    }

    public void Setup()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true); 
    }

    public void IncreaseAttackSpeed()
    {
        Time.timeScale = 1;
        playerConfig.timeBetweenBullets -= 0.01f;
        gameObject.SetActive(false);
    }

    public void IncreaseDiagonalWeapon()
    {
        Time.timeScale = 1;
        playerConfig.raysCount += 2;
        gameObject.SetActive(false);
    }

    public void IncreaseAttackRange()
    {
        Time.timeScale = 1;
        playerConfig.shootingRange += 1;
        gameObject.SetActive(false);
    }

}
