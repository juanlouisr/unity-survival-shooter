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

    void Awake()
    {
        var orbTypes = System.Enum.GetValues(typeof(OrbType));
        orbType = (OrbType)Random.Range(0, orbTypes.Length);
        gameObject.GetComponent<Renderer>().material = materials[(int)orbType];
    }

    void Take()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player detected");
            Destroy(gameObject);
        }
    }





}
