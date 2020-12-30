﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonBehaviour : MonoBehaviour
{
    [SerializeField] private float _ratCheckRadius = 20;
    [SerializeField] private float _walkingSpeed = 1.0f;
    [SerializeField] private float _runningSpeed = 2.5f;
    [SerializeField] private float _waypointTagSqrDistance = .5f;
    [SerializeField] private float _fearDuration = 0.5f;

    private RatDetector _ratDetector;
    private float _movementSpeed;
    private GameObject[] _allNodes;//for finding nearest node
    private float _fearTimeout = 0.0f;
    private bool _isFeared = false;
    private bool _isCharmed = false;
    private WalkableNode _charmSource;
    private List<WalkableNode> _pathToWitch;
    private int _nextNodeOnPath;
    private Vector2 _direction;


    void Start()
    {
        EventManager.GetInstance().RegisterCharmEvent(OnCharm);
        EventManager.GetInstance().RegisterWitchDespawnEvent(OnWitchDespawn);
        _allNodes = GameObject.FindGameObjectsWithTag("Waypoint");
        _movementSpeed = _walkingSpeed;
        _ratDetector = GetComponentInChildren<RatDetector>();
        _ratDetector.GetComponent<CircleCollider2D>().radius = _ratCheckRadius;
        TryToFixate();
    }

    void Update()
    {
        if (_fearTimeout <= Time.time) {//only change state if not feared

            //if nearby rats, become afraid
            if (_ratDetector.RatsNearby()) {
                _isFeared = true;
                _movementSpeed = _runningSpeed;
                _direction = _ratDetector.RunningFromRatsDirection();
                _fearTimeout = Time.time + _fearDuration;
                return;
            } else {
                if (_isFeared) {
                    _isFeared = false;
                    TryToFixate();
                }
                _movementSpeed = _walkingSpeed;
                if (_isCharmed) {
                    WalkableNode nextWaypoint = _pathToWitch[_nextNodeOnPath];

                    //if we're close enough to a waypoint, we'll move to the next one
                    if (Vector2.SqrMagnitude(transform.position - nextWaypoint.transform.position) < _waypointTagSqrDistance
                                    && _nextNodeOnPath < _pathToWitch.Count - 1) {
                        _nextNodeOnPath++;
                        nextWaypoint = _pathToWitch[_nextNodeOnPath];
                    }
                    _direction = (nextWaypoint.transform.position - transform.position).normalized;
                } else {
                    //if not charmed and no rats, nearby, nothing
                    _direction = Vector2.zero;
                }
            }
        }
    }

    //finds closest witch and fixates on it
    private void TryToFixate() {

        GameObject[] witches = GameObject.FindGameObjectsWithTag("Witch");
        float minWitchDistance = float.MaxValue;
        GameObject closestWitch = null;

        //find closest witch
        foreach (GameObject witch in witches) {
            float witchDistance = Vector2.SqrMagnitude(witch.transform.position - transform.position);
            if (witch.GetComponent<WitchBehaviour>().IsCasting() && witchDistance < minWitchDistance) {
                minWitchDistance = witchDistance;
                closestWitch = witch;
            }
        }

        //charm towards closest witch
        if (closestWitch != null) {
            OnCharm(closestWitch.GetComponent<WitchBehaviour>().NodeLocation);
        }
    }

    private void FixedUpdate()
    {
        // move towards movementDirection
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + _direction, _movementSpeed * Time.deltaTime);
    }
    private void OnCharm(WalkableNode location) {
        if (_isCharmed) {
            return;
        }
        _isCharmed = true;
        _charmSource = location;

        //find closest waypoint to person
        WalkableNode closestNode = null;
        float minSquareDistance = float.MaxValue;
        foreach (GameObject node in _allNodes) {
            float nodeSquareDistance = Vector2.SqrMagnitude(transform.position - node.transform.position);
            if (nodeSquareDistance < minSquareDistance) {
                minSquareDistance = nodeSquareDistance;
                closestNode = node.GetComponent<WalkableNode>();
            }
        }

        _pathToWitch = closestNode.RouteToTarget(_charmSource);
        _nextNodeOnPath = 0;

    }

    private void OnWitchDespawn() {
        _isCharmed = false;
    }

    public void Die() {
        EventManager.GetInstance().UnregisterCharmEvent(OnCharm);
        EventManager.GetInstance().UnregisterWitchDespawnEvent(OnWitchDespawn);
        _ratDetector.Die();
        Destroy(gameObject);
    }
}
