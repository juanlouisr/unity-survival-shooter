using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberAttack : EnemyAttack
{
    // Start is called before the first frame update
    
    void Attack()
    {
        timer = 0f;

        // Taking damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        Destroy(gameObject);
    }
}
