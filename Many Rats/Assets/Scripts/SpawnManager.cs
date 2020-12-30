using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Camera Camera;
    [SerializeField] private GameObject personPrefab;
    [SerializeField] private GameObject witchPrefab;
    [SerializeField] private GameObject carriagePrefab;
    [SerializeField] private float witchSpawnTime;
    [SerializeField] private float carriageSpawnTime;

    public GameObject WitchSpawnsParentObject;
    public GameObject PersonSpawnsParentObject;
    public WalkableNode[] CarriageSpawns;
    public float MaxPersonSpawnTime = 3;
    public float WitchSpawn = 5;

    private WalkableNode[] _witchSpawns;
    private List<Transform> _personSpawns;
    private float _nextPersonSpawnTime;
    private float _nextCarriageSpawnTime;
    private float _nextWitchSpawnTime;
    private GameObject witchObject;
    private GameObject carriageObject;

    void Start()
    {
        _witchSpawns = WitchSpawnsParentObject.GetComponentsInChildren<WalkableNode>();
        _personSpawns = new List<Transform>();
        foreach (Transform child in PersonSpawnsParentObject.transform) {
            _personSpawns.Add(child);
        }
        _nextPersonSpawnTime = Random.Range(0.0f, MaxPersonSpawnTime);
        _nextWitchSpawnTime = WitchSpawn;
    }

    void Update()
    {
        if (Time.time > _nextPersonSpawnTime) {
            SpawnPerson();
            _nextPersonSpawnTime += Random.Range(0.0f, MaxPersonSpawnTime);
        }

        if (Time.time > _nextWitchSpawnTime) {
            SpawnWitch();
            _nextWitchSpawnTime += WitchSpawn;
        }
    }

    /*
     * Finds a random walkable node and spawns the witch there.  Updates the witch to have access to the
     * spawn node. 
     */
    private void SpawnWitch() {
        int minNodeIndex = 0;
        int maxNodeIndex = _witchSpawns.Length;
        WalkableNode spawnLocation = _witchSpawns[Random.Range(minNodeIndex, maxNodeIndex)];
        GameObject witchContainer = GameObject.Find("Actors/Witch");

        //remove any existing witches
        foreach (Transform w in witchContainer.transform) {
            w.GetComponent<WitchBehaviour>().Die();
        }
        GameObject witch = Instantiate(witchPrefab, spawnLocation.transform.position, Quaternion.identity, GameObject.Find("Actors/Witch").transform);
        witch.GetComponent<WitchBehaviour>().SetNode(spawnLocation);
    }

    /*
     * Finds a random person spawn location and spawns a player there.
     */
    private void SpawnPerson() {
        int minNodeIndex = 0;
        int maxNodeIndex = _personSpawns.Count;
        Transform spawnLocation = _personSpawns[Random.Range(minNodeIndex, maxNodeIndex)];
        Instantiate(personPrefab, spawnLocation.position, Quaternion.identity, GameObject.Find("Actors/Persons").transform);
    }
}
