using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHordeBehaviour : MonoBehaviour
{
    [SerializeField] private float _personDetectorRadius;
    [SerializeField] private float _runSpeed = 2.0f;
    [SerializeField] private float _waypointTagSqrDistance = .1f;
    [SerializeField] private float _chaseTimeout = 5;//if it hasn't found target for this long, it'll reset its behavior

    private enum HordeState
    {
        Waiting, //awaiting orders
        Pathing, //following waypoint movement commands
        Chasing  //following target directly
    }

    private PersonDetector _personDetector;
    private GameObject[] _allNodes;     //for finding nearest node
    private GameObject _targetPerson;   //the kill target
    private WalkableNode _targetNode;   //node closest to kill target at fixate time
    private HordeState _currentState = HordeState.Waiting;      //when pathing along waypoints
    private float _movementSpeed;
    private float _endChaseTime;

    void Start()
    {
        EventManagerOneArg<SpawnRatHordeEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
        EventManagerOneArg<DespawnPersonEvent, GameObject>.GetInstance().AddListener(OnPersonDespawn);
        _personDetector = GetComponentInChildren<PersonDetector>();
        _personDetector.GetComponent<CircleCollider2D>().radius = _personDetectorRadius;
        _personDetector.ratirl = this;
        _allNodes = GameObject.FindGameObjectsWithTag("Waypoint");
        _movementSpeed = _runSpeed;
    }

    private void Update() {

        //when a waypoint is reached, find next waypoint
        if (_currentState == HordeState.Pathing && Vector2.SqrMagnitude(transform.position - _targetNode.transform.position) < _waypointTagSqrDistance) {
            PathToClosest();
        }

        if (_currentState == HordeState.Waiting) {
            PathToClosest();
        }

        if (_currentState == HordeState.Chasing && _endChaseTime < Time.time) {
            _currentState = HordeState.Waiting;
        }
    }

    private void FixedUpdate()
    {
        if (_currentState == HordeState.Chasing) {
            transform.position = Vector2.MoveTowards(transform.position, _targetPerson.transform.position, _movementSpeed * Time.deltaTime);
        } else if (_currentState == HordeState.Pathing) {
            transform.position = Vector2.MoveTowards(transform.position, _targetNode.transform.position, _movementSpeed * Time.deltaTime);
        }
    }

    private void OnPersonDespawn(GameObject person) {
        if(person == _targetPerson) {
            _currentState = HordeState.Waiting;
        }
    }
    private void PathToClosest() {
        _targetPerson = FindClosestPerson();
        if (_targetPerson != null) {
            List<WalkableNode> route = RouteToTarget(_targetPerson);
            if (route.Count > 1) {//nontrivial route
                _targetNode = route[1];//first nontrivial node (next node in route)
                _currentState = HordeState.Pathing;
            } else {
                _currentState = HordeState.Chasing;
                _endChaseTime = Time.time + _chaseTimeout;
            }
        } else {
            _currentState = HordeState.Waiting;
        }
    }

    //finds the nearest person to the target, and sets route to target
    private GameObject FindClosestPerson() {
        GameObject[] persons = GameObject.FindGameObjectsWithTag("Person");
        float minPersonDistance = float.MaxValue;
        GameObject closestPerson = null;

        //find closest person
        foreach (GameObject person in persons) {
            float personDistance = Vector2.SqrMagnitude(person.transform.position - transform.position);
            if (personDistance < minPersonDistance) {
                minPersonDistance = personDistance;
                closestPerson = person;
            }
        }
        return closestPerson;
    }

    private List<WalkableNode> RouteToTarget(GameObject person) { 

        //find closest node to person
        if (person != null) {
            _targetNode = person.GetComponent<PersonBehaviour>().ClosestNode();
        }

        GameObject[] nodes = GameObject.FindGameObjectsWithTag("Waypoint");

        //find closest node
        WalkableNode closestNode = null;
        float minSquareDistance = float.MaxValue;
        foreach (GameObject node in _allNodes) {
            float nodeSquareDistance = Vector2.SqrMagnitude(transform.position - node.transform.position);
            if (nodeSquareDistance < minSquareDistance) {
                minSquareDistance = nodeSquareDistance;
                closestNode = node.GetComponent<WalkableNode>();
            }
        }

        //calculate route to target node from closest node
        return closestNode.RouteToTarget(_targetNode);
    }

    public void Kill(PersonBehaviour person) {
        person.Die();
    }

    public void Die() {
        EventManagerOneArg<DespawnRatHordeEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
        EventManagerOneArg<DespawnPersonEvent, GameObject>.GetInstance().RemoveListener(OnPersonDespawn);
        Destroy(gameObject);
    }
}
