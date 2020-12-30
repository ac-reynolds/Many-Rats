using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _carriagePrefab;
    [SerializeField] private GameObject _personPrefab;
    [SerializeField] private GameObject _ratPrefab;
    [SerializeField] private GameObject _witchPrefab;

    [SerializeField] private GameObject _carriageSpawnLocationsParent;
    [SerializeField] private GameObject _personSpawnLocationsParent;
    [SerializeField] private GameObject _witchSpawnLocationsParent;

    [SerializeField] private float _carriageSpawnTime = 0;
    [SerializeField] private float _maxPersonSpawnTime = 3;
    [SerializeField] private float _witchSpawnTime = 5;

    private WalkableNode[] _carriageSpawns;
    private List<Transform> _personSpawns;
    private WalkableNode[] _witchSpawns;
    private float _nextPersonSpawnTime;
    private float _nextCarriageSpawnTime;
    private float _nextWitchSpawnTime;

    void Start()
    {
        _carriageSpawns = _carriageSpawnLocationsParent.GetComponentsInChildren<WalkableNode>();
        _personSpawns = new List<Transform>();
        foreach (Transform child in _personSpawnLocationsParent.transform) {
            _personSpawns.Add(child);
        }
        _witchSpawns = _witchSpawnLocationsParent.GetComponentsInChildren<WalkableNode>();

        _nextPersonSpawnTime = Random.Range(0.0f, _maxPersonSpawnTime);
        _nextWitchSpawnTime = _witchSpawnTime;
        EventManager.GetInstance().RegisterRatSpawnRequestEvent(SpawnRat);
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
    }


    /*
     * Finds a random person spawn location and spawns a player there.
     */
    private void SpawnPerson() {
        int minNodeIndex = 0;
        int maxNodeIndex = _personSpawns.Count;
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
     * Finds a random walkable node and spawns the witch there.  Updates the witch to have access to the
     * spawn node. 
     */
    private void SpawnWitch() {
        int minNodeIndex = 0;
        int maxNodeIndex = _witchSpawns.Length;
        WalkableNode spawnLocation = _witchSpawns[Random.Range(minNodeIndex, maxNodeIndex)];
        GameObject witch = Instantiate(_witchPrefab, spawnLocation.transform.position, Quaternion.identity, GameObject.Find("Actors/Witch").transform);
        witch.GetComponent<WitchBehaviour>().SetNode(spawnLocation);
    }

}
