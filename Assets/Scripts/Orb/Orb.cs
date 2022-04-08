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
    public float multiplier = 1.2f;

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
        }
    }





}
