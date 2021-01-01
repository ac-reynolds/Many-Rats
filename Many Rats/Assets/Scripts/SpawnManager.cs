using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject CarriagePrefab;
    public GameObject PersonPrefab;
    public GameObject RatPrefab;
    public GameObject RatHordePrefab;
    public GameObject WitchPrefab;

    public GameObject CarriageSpawnLocationsParentObject;
    public GameObject PersonSpawnLocationsParentObject;
    public GameObject WitchSpawnLocationsParentObject;

    public float CarriageSpawnTime = 2;
    public float MaxPersonSpawnTime = 3;
    public float WitchSpawnTime = 5;

    private List<WalkableNode> _carriageSpawns;
    private List<Transform> _personSpawns;
    private List<WalkableNode> _witchSpawns;
    private float _nextPersonSpawnTime;
    private float _nextCarriageSpawnTime;
    private float _nextWitchSpawnTime;

    void Start()
    {
        _carriageSpawns = new List<WalkableNode>();
        _personSpawns = new List<Transform>();
        _witchSpawns = new List<WalkableNode>();

        _carriageSpawns.AddRange(CarriageSpawnLocationsParentObject.GetComponentsInChildren<WalkableNode>());
        foreach (Transform child in PersonSpawnLocationsParentObject.transform) {
            _personSpawns.Add(child);
        }
        _witchSpawns.AddRange(WitchSpawnLocationsParentObject.GetComponentsInChildren<WalkableNode>());


        _nextPersonSpawnTime = Random.Range(0.0f, MaxPersonSpawnTime);
        _nextWitchSpawnTime = WitchSpawnTime;
        EventManagerOneArg<RequestSpawnRatEvent, Vector2>.GetInstance().AddListener(SpawnRat);
        EventManagerOneArg<RequestSpawnRatHordeEvent, Vector2>.GetInstance().AddListener(SpawnRatHorde);
        EventManagerOneArg<DespawnWitchEvent, GameObject>.GetInstance().AddListener(OnWitchDespawn);
    }

    void Update()
    {
        if (Time.time > _nextPersonSpawnTime) {
            SpawnPerson();
            _nextPersonSpawnTime += Random.Range(0.0f, MaxPersonSpawnTime);
        }

        if (Time.time > _nextWitchSpawnTime) {
            SpawnWitch();
            _nextWitchSpawnTime += WitchSpawnTime;
        }

        if (Time.time > _nextCarriageSpawnTime) {
            SpawnCarriage();
            _nextCarriageSpawnTime += CarriageSpawnTime;
        }
    }

    private void OnWitchDespawn(GameObject witch) {
        _witchSpawns.Add(witch.GetComponent<WitchBehaviour>().NodeLocation);
    }

    
    private void SpawnCarriage() {
        if(_carriageSpawns.Count == 0) {
            return;
        }
        int minNodeIndex = 0;
        int maxNodeIndex = _carriageSpawns.Count;
        WalkableNode spawnLocation = _carriageSpawns[Random.Range(minNodeIndex, maxNodeIndex)];
        GameObject carriage = Instantiate(CarriagePrefab, spawnLocation.transform.position, Quaternion.identity, GameObject.Find("Actors/Carriages").transform);
        carriage.GetComponent<CarriageBehaviour>().NodeLocation = spawnLocation;
        _carriageSpawns.Remove(spawnLocation);
    }


    /*
     * Finds a random person spawn location and spawns a player there.
     */
    private void SpawnPerson() {
        int minNodeIndex = 0;
        int maxNodeIndex = _personSpawns.Count;
        Transform spawnLocation = _personSpawns[Random.Range(minNodeIndex, maxNodeIndex)];
        Instantiate(PersonPrefab, spawnLocation.position, Quaternion.identity, GameObject.Find("Actors/Persons").transform);
    }

    /*
     * Spanws a rat at the specified point.
     */
    private void SpawnRat(Vector2 boardPosition) {
        Instantiate(RatPrefab, boardPosition, Quaternion.identity, GameObject.Find("Actors/Rats").transform);
    }

    /*
     * Spawns a rat horde at the specified point.
     */
    private void SpawnRatHorde(Vector2 boardPosition) {
        Instantiate(RatHordePrefab, boardPosition, Quaternion.identity, GameObject.Find("Actors/RatHordes").transform);
    }

    /*
     * Finds a random walkable node and spawns the witch there.  Updates the witch to have access to the
     * spawn node. 
     */
    private void SpawnWitch() {
        if (_witchSpawns.Count == 0) {
            return;
        }
        int minNodeIndex = 0;
        int maxNodeIndex = _witchSpawns.Count;
        int rand = Random.Range(minNodeIndex, maxNodeIndex);
        WalkableNode spawnLocation = _witchSpawns[rand];
        GameObject witch = Instantiate(WitchPrefab, spawnLocation.transform.position, Quaternion.identity, GameObject.Find("Actors/Witch").transform);
        witch.GetComponent<WitchBehaviour>().NodeLocation = spawnLocation;
        _witchSpawns.Remove(spawnLocation);
    }
}
