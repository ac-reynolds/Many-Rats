using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private GameObject witchPrefab;
    [SerializeField] private GameObject carriagePrefab;
    [SerializeField] private float personSpawnTime;
    [SerializeField] private float witchSpawnTime;
    [SerializeField] private float carriageSpawnTime;
    private float personSpawnTimer;
    private float witchSpawnTimer;
    private float carriageSpawnTimer;
    private GameObject witchObject;
    private GameObject carriageObject;

    void Start()
    {
        return;
        // initially spawn 3 people, 1 carriage, 1 witch
        // mark spawn times
        personSpawnTimer = Time.time;
        witchSpawnTimer = Time.time;
        carriageSpawnTimer = Time.time;
        Instantiate(carriagePrefab, GetNewSpawnCoords(), Quaternion.identity);
        Instantiate(witchPrefab, GetNewSpawnCoords(), Quaternion.identity);
        Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
        Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
        Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
    }

    void Update()
    {
        return;
        // after personSpawnTime seconds, spawn 3 new people and mark new spawn time
        if(Time.time - personSpawnTime > personSpawnTimer)
        {
            Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
            Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
            Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
            personSpawnTimer = Time.time;
        }
        // after witchSpawnTime seconds, delete old witch, spawn new witch, and mark new spawn time
        if (Time.time - witchSpawnTime > witchSpawnTimer)
        {
            witchObject = GameObject.FindGameObjectWithTag("Witch");
            Destroy(witchObject);
            Instantiate(witchPrefab, GetNewSpawnCoords(), Quaternion.identity);
            witchSpawnTimer = Time.time;
        }
        // after carriageSpawnTime seconds, delete old carriage, spawn new carriage, and mark new spawn time
        if (Time.time - carriageSpawnTime > carriageSpawnTimer)
        {
            carriageObject = GameObject.FindGameObjectWithTag("Carriage");
            Destroy(carriageObject);
            Instantiate(carriagePrefab, GetNewSpawnCoords(), Quaternion.identity);
            carriageSpawnTimer = Time.time;
        }
    }

    /*
     * Returns a random point on the playing field in view of the camera.  Evenly distributed over x and y axes.  
     */
    private Vector3 GetNewSpawnCoords()
    {
        float randomScreenx = Random.Range(0, Camera.pixelWidth);
        float randomScreeny = Random.Range(0, Camera.pixelHeight);
        Vector3 randomScreenSpawn = new Vector3(randomScreenx, randomScreeny, 0);
        Ray cameraRay = Camera.ScreenPointToRay(randomScreenSpawn);
        Vector3 cameraRayOrigin = cameraRay.origin;
        Vector3 cameraRayDirection = cameraRay.direction;
        return cameraRayOrigin - cameraRayOrigin.z / cameraRayDirection.z * cameraRayDirection;
    }
}
