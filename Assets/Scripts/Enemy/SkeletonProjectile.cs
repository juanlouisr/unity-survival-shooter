using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonProjectile : MonoBehaviour
{
    // Start is called before the first frame update

    public int damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var audio = GetComponent<AudioSource>();
            audio.Play();
            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, audio.clip.length);


            var playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }
}
