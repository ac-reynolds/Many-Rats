using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _carriagePrefab;
    [SerializeField] private GameObject _personPrefab;
    [SerializeField] private GameObject _ratPrefab;
    [SerializeField] private GameObject _ratHordePrefab;
    [SerializeField] private GameObject _witchPrefab;

    [SerializeField] private GameObject _carriageSpawnLocationsParent;
    [SerializeField] private GameObject _personSpawnLocationsParent;
    [SerializeField] private GameObject _witchSpawnLocationsParent;

    [SerializeField] private float _carriageSpawnTime = 2;
    [SerializeField] private float _maxPersonSpawnTime = 3;
    [SerializeField] private float _witchSpawnTime = 5;

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

        _carriageSpawns.AddRange(_carriageSpawnLocationsParent.GetComponentsInChildren<WalkableNode>());
        foreach (Transform child in _personSpawnLocationsParent.transform) {
            _personSpawns.Add(child);
        }
        _witchSpawns.AddRange(_witchSpawnLocationsParent.GetComponentsInChildren<WalkableNode>());


        _nextPersonSpawnTime = Random.Range(0.0f, _maxPersonSpawnTime);
        _nextWitchSpawnTime = _witchSpawnTime;
        EventManager.GetInstance().RegisterRatSpawnRequestEvent(SpawnRat);
        EventManager.GetInstance().RegisterRatHordeSpawnRequestEvent(SpawnRatHorde);
    }

    void Update()
    {
        if (Time.time > _nextPersonSpawnTime) {
            SpawnPerson();
            _nextPersonSpawnTime += Random.Range(0.0f, _maxPersonSpawnTime);
        }

        if (Time.time > _nextWitchSpawnTime) {
            SpawnWitch();
            _nextWitchSpawnTime += _witchSpawnTime;
        }

        if (Time.time > _nextCarriageSpawnTime) {
            SpawnCarriage();
            _nextCarriageSpawnTime += _carriageSpawnTime;
        }
    }

    private void OnWitchDespawn(WalkableNode node) {
        _witchSpawns.Add(node);
    }

    
    private void SpawnCarriage() {
        if(_carriageSpawns.Count == 0) {
            return;
        }
        int minNodeIndex = 0;
        int maxNodeIndex = _carriageSpawns.Count;
        WalkableNode spawnLocation = _carriageSpawns[Random.Range(minNodeIndex, maxNodeIndex)];
        GameObject carriage = Instantiate(_carriagePrefab, spawnLocation.transform.position, Quaternion.identity, GameObject.Find("Actors/Carriages").transform);
        carriage.GetComponent<CarriageBehaviour>().NodeLocation = spawnLocation;
        _carriageSpawns.Remove(spawnLocation);
    }


    /*
     * Finds a random person spawn location and spawns a player there.
     */
    private void SpawnPerson() {
        int minNodeIndex = 0;
        int maxNodeIndex = _personSpawns.Count;

        List<WalkableNode> _viableSpawns = new List<WalkableNode>();
        Transform spawnLocation = _personSpawns[Random.Range(minNodeIndex, maxNodeIndex)];
        Instantiate(_personPrefab, spawnLocation.position, Quaternion.identity, GameObject.Find("Actors/Persons").transform);
    }

    /*
     * Spanws a rat at the specified point.
     */
    private void SpawnRat(Vector2 boardPosition) {
        Instantiate(_ratPrefab, boardPosition, Quaternion.identity, GameObject.Find("Actors/Rats").transform);
    }

    /*
     * Spawns a rat horde at the specified point.
     */
    private void SpawnRatHorde(Vector2 boardPosition) {
        Instantiate(_ratHordePrefab, boardPosition, Quaternion.identity, GameObject.Find("Actors/RatHordes").transform);
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
        GameObject witch = Instantiate(_witchPrefab, spawnLocation.transform.position, Quaternion.identity, GameObject.Find("Actors/Witch").transform);
        witch.GetComponent<WitchBehaviour>().NodeLocation = spawnLocation;
        _witchSpawns.Remove(spawnLocation);
    }

}
