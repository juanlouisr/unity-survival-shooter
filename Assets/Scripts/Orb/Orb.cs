using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbType { Health, Power, Speed }

public class Orb : MonoBehaviour
{

    [SerializeField]
    Material[] materials = new Material[System.Enum.GetValues(typeof(OrbType)).Length];
    // Start is called before the first frame update
    public OrbType orbType = OrbType.Health;
    public float multiplier = 0.1f;

    [SerializeField] float orbUpTime = 10f;

    private Renderer rend;

    void Awake()
    {
        var orbTypes = System.Enum.GetValues(typeof(OrbType));
        orbType = (OrbType)Random.Range(0, orbTypes.Length);
        rend = gameObject.GetComponent<Renderer>();
        rend.material = materials[(int)orbType];
    }

    void Start()
    {
        Destroy(gameObject, orbUpTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var audio = GetComponent<AudioSource>();
            audio.Play();
            rend.enabled = false;
            Destroy(gameObject, audio.clip.length);

            var playerData = other.GetComponent<PlayerConfig>();

            switch (orbType)
            {
                case OrbType.Health:
                    var playerHealth = other.GetComponent<PlayerHealth>();
                    playerHealth.currentHealth += multiplier * playerData.maxHealth;
                    playerHealth.currentHealth = Mathf.Clamp(playerHealth.currentHealth, 0, playerData.maxHealth);
                    break;

                case OrbType.Power:
                    playerData.attackPower += multiplier * playerData.MaxAttackPower;
                    playerData.attackPower = Mathf.Clamp(playerData.attackPower, 0, playerData.MaxAttackPower);
                    break;

                case OrbType.Speed:
                    playerData.moveSpeed += multiplier * playerData.MaxSpeed;
                    playerData.moveSpeed = Mathf.Clamp(playerData.moveSpeed, 0, playerData.MaxSpeed);
                    break;


            }
        }
    }





}
