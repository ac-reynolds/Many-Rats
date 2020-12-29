﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private GameObject witchPrefab;
    [SerializeField] private GameObject carriagePrefab;
    [SerializeField] private float personSpawnTime;
    [SerializeField] private int personsSpawnedPerWave = 3;
    [SerializeField] private float witchSpawnTime;
    [SerializeField] private float carriageSpawnTime;
    private float personSpawnTimer;
    private float witchSpawnTimer;
    private float carriageSpawnTimer;
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
            SpawnPeople(personsSpawnedPerWave);
            personSpawnTimer = Time.time;
        }
        if (Time.time - witchSpawnTime > witchSpawnTimer)
        {
            SpawnWitch();
            witchSpawnTimer = Time.time;
        }
        if (Time.time - carriageSpawnTime > carriageSpawnTimer)
        {
            SpawnCarriage();
            carriageSpawnTimer = Time.time;
        }
    }

    private void SpawnPeople(int n) {
        for (int i = 0; i < n; i++) {
            Instantiate(personPrefab, GetNewSpawnCoords(), Quaternion.identity);
        }
    }

    private void SpawnWitch() {
        witchObject = GameObject.FindGameObjectWithTag("Witch");
        Destroy(witchObject);
        Instantiate(witchPrefab, GetNewSpawnCoords(), Quaternion.identity);
    }

    private void SpawnCarriage() {
        carriageObject = GameObject.FindGameObjectWithTag("Carriage");
        Destroy(carriageObject);
        Instantiate(carriagePrefab, GetNewSpawnCoords(), Quaternion.identity);
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
        Vector3 prospectivePosition = cameraRayOrigin - cameraRayOrigin.z / cameraRayDirection.z * cameraRayDirection;

        //TODO don't spawn inside collider
        //Debug.Log(GetComponentsInChildren<Collider>().Length);

        return prospectivePosition;
    }
}
