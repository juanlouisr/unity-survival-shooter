using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject orbPrefab;
    [SerializeField] float spawnDelayTime = 2f;
    [SerializeField] public bool isLooping = false;

    List<Transform> spawnPoints;
    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints = GetWayPoints();
    }
    void Start()
    {
        StartCoroutine(SpawnOrb());
    }
    IEnumerator SpawnOrb()
    {
        while (isLooping)
        {
            var spawnPointIdx = Random.Range(0, spawnPoints.Count);
            var point = spawnPoints[spawnPointIdx];
            Instantiate(orbPrefab, point.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }

    public List<Transform> GetWayPoints()
    {
        var transforms = new List<Transform>();
        foreach (Transform child in spawnPoint)
        {
            transforms.Add(child);
        }
        return transforms;
    }
}
