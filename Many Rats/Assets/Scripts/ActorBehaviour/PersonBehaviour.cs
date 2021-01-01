using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonBehaviour : MonoBehaviour
{
    [SerializeField] private float _ratCheckRadius = 2;
    [SerializeField] private float _walkingSpeed = 1.0f;
    [SerializeField] private float _runningSpeed = 2.5f;
    [SerializeField] private float _waypointTagSqrDistance = .5f;
    [SerializeField] private float _fearDuration = 3.0f;

    private RatDetector _ratDetector;
    private float _movementSpeed;
    private GameObject[] _allNodes;//for finding nearest node
    private float _fearTimeout = 0.0f;//time when rat fear should end
    private bool _isFeared = false;
    private bool _isCharmed = false;
    private WalkableNode _charmSource;
    private List<WalkableNode> _pathToWitch;
    private int _nextNodeOnPath;
    private Vector2 _direction;

    /* 
     * On spawn, checks for witches, fixating and following the closest one.  If the target witch despawns,
     * attempt to refixate, going idle if no witch is available.  
     * Refixate when a new witch spawns.  
     * When there are rats nearby, runs from rats.  
     */
    void Start()
    {
        EventManagerOneArg<SpawnPersonEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
        EventManagerOneArg<SpawnWitchEvent, GameObject>.GetInstance().AddListener(OnWitchSpawn);
        EventManagerOneArg<DespawnWitchEvent, GameObject>.GetInstance().AddListener(OnWitchDespawn);
        _allNodes = GameObject.FindGameObjectsWithTag("Waypoint");
        _movementSpeed = _walkingSpeed;
        _ratDetector = GetComponentInChildren<RatDetector>();
        _ratDetector.GetComponent<CircleCollider2D>().radius = _ratCheckRadius;
    }


    //When person is not feared, they will check for nearby rats, then proceed to the witch if possible.  If no witch is avail, they'll
    //time out and move to the nearest waypoint.
    void Update() {

        if(_isCharmed) {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        } else if (Time.time < _fearTimeout) {
            GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        } else {
            GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }

        if (_isFeared && Time.time < _fearTimeout) {//currently feared

            _isCharmed = false;

        } else {

            //check for rats
            if (_ratDetector.NumNearbyRats() > 0) {
                _direction = _ratDetector.RunningFromRatsDirection();
                _isFeared = true;
                _fearTimeout = Time.time + _fearDuration;
                _movementSpeed = _runningSpeed;
            } else {//no rats, past or present
                _isFeared = false;
                if (_isCharmed) {
                    _direction = GetDirectionAlongPath();
                } else {
                    _direction = Vector2.zero;
                    TryToFixate();
                }
            }
        }
    }

    private void FixedUpdate() {
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + _direction, _movementSpeed * Time.deltaTime);
    }

    private Vector2 GetDirectionAlongPath() {

        WalkableNode nextWaypoint = _pathToWitch[_nextNodeOnPath];
        //if we're close enough to a waypoint, we'll move to the next one
        if (Vector2.SqrMagnitude(transform.position - nextWaypoint.transform.position) < _waypointTagSqrDistance
                        && _nextNodeOnPath < _pathToWitch.Count - 1) {
            _nextNodeOnPath++;
            nextWaypoint = _pathToWitch[_nextNodeOnPath];
        }
        return (nextWaypoint.transform.position - transform.position).normalized;
    }

    /*
     * First finds the closest witch.  Then finds the closest node, and paths from that node to the witch
     */
    private void TryToFixate() {
        Debug.Log("trying to fixate");
        //find closest witch
        GameObject[] witches = GameObject.FindGameObjectsWithTag("Witch");
        float minWitchDistance = float.MaxValue;
        GameObject closestWitch = null;
        foreach (GameObject witch in witches) {
            float witchDistance = Vector2.SqrMagnitude(witch.transform.position - transform.position);
            if (witch.GetComponent<WitchBehaviour>().IsCasting() && witchDistance < minWitchDistance) {
                
                //if we already have a charm source, don't use it anymore 
                if (_charmSource != null) {
                    if (witch.GetComponent<WitchBehaviour>().NodeLocation != _charmSource) {
                        minWitchDistance = witchDistance;
                        closestWitch = witch;
                    }
                } else {
                    minWitchDistance = witchDistance;
                    closestWitch = witch;
                }
            }
        }

        Debug.Log("trying to fixate on closestwitch" + closestWitch);

        if (closestWitch != null) {
            _charmSource = closestWitch.GetComponent<WitchBehaviour>().NodeLocation;
            _isCharmed = true;
            _nextNodeOnPath = 0;
            _pathToWitch = ClosestNode().RouteToTarget(_charmSource);
        }
        
    }

    public WalkableNode ClosestNode() {
        WalkableNode closestNode = null;
        float minSquareDistance = float.MaxValue;
        foreach (GameObject node in _allNodes) {
            float nodeSquareDistance = Vector2.SqrMagnitude(transform.position - node.transform.position);
            if (nodeSquareDistance < minSquareDistance) {
                minSquareDistance = nodeSquareDistance;
                closestNode = node.GetComponent<WalkableNode>();
            }
        }
        return closestNode;
    }

    //reset charm status
    private void OnWitchSpawn(GameObject witch) {
        _isCharmed = false;
    }

    private void OnWitchDespawn(GameObject witch) {
        _isCharmed = false;
    }

    public void Die() {
        EventManagerOneArg<DespawnPersonEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
        EventManagerOneArg<SpawnWitchEvent, GameObject>.GetInstance().RemoveListener(OnWitchSpawn);
        EventManagerOneArg<DespawnWitchEvent, GameObject>.GetInstance().RemoveListener(OnWitchDespawn);
        _ratDetector.Die();
        Destroy(gameObject);
    }
}
