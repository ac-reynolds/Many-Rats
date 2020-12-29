using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WitchBehaviour : MonoBehaviour
{
    public float CastDelay = 5; //amount of time after spawning until the witch casts

    public WalkableNode _nodeLocation;
    private float _castTime;
    private bool _readyToCast;

    void Start()
    {
        _castTime = Time.time + CastDelay;
        _readyToCast = true;
    }

    public void SetNode(WalkableNode node) {
        _nodeLocation = node;
    }

    private void CharmAllPersons() {
        foreach (GameObject person in GameObject.FindGameObjectsWithTag("Person")) {
            person.GetComponent<PersonBehaviour>().OnCharm(_nodeLocation);
        }
    }

    private void Update() {
        if (_readyToCast && _castTime < Time.time) {
            _readyToCast = false;
            CharmAllPersons();
        }

    }

}
