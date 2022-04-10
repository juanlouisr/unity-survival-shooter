using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject weaponUpgradeMenuObj;


    [SerializeField] float timeBetweenUpgrade = 10;

    public void Start()
    {
        StartCoroutine(upgradeWeapon());
    }
    
    IEnumerator upgradeWeapon()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenUpgrade);
            weaponUpgradeMenuObj.GetComponent<WeaponUpgradeMenu>().Setup();
        }
    }

}
