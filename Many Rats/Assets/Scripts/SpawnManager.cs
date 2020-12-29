using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private GameObject witchPrefab;
    [SerializeField] private GameObject carriagePrefab;
    [SerializeField] private float personSpawnTime;
    [SerializeField] private float witchSpawnTime;
    [SerializeField] private float carriageSpawnTime;
    [SerializeField] private float spawnRadius;
    private float personSpawnTimer;
    private float witchSpawnTimer;
    private float carriageSpawnTimer;
    private Vector3 spawnLocation;
    private GameObject witchObject;
    private GameObject carriageObject;

    // Start is called before the first frame update
    void Start()
    {
        personSpawnTimer = Time.time;
        witchSpawnTimer = Time.time;
        carriageSpawnTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - personSpawnTime > personSpawnTimer)
        {
            Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
            Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
            Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
            personSpawnTimer = Time.time;
        }
        if (Time.time - witchSpawnTime > witchSpawnTimer)
        {
            witchObject = GameObject.FindGameObjectWithTag("Witch");
            Destroy(witchObject);
            Instantiate(witchPrefab, GetNewSpawnCoords(), Quaternion.identity);
            witchSpawnTimer = Time.time;
        }
        if (Time.time - carriageSpawnTime > carriageSpawnTimer)
        {
            carriageObject = GameObject.FindGameObjectWithTag("Carriage");
            Destroy(carriageObject);
            Instantiate(carriagePrefab, GetNewSpawnCoords(), Quaternion.identity);
            carriageSpawnTimer = Time.time;
        }
    }

    private Vector3 GetNewSpawnCoords()
    {
        spawnLocation = Random.insideUnitCircle * spawnRadius;
        return spawnLocation;
    }
}
