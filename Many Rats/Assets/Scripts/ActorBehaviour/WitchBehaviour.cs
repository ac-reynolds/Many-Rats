using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WitchBehaviour : MonoBehaviour
{
    public float CastDelay = 1; //amount of time after spawning until the witch casts

    public WalkableNode NodeLocation;
    private float _castTime;
    private bool _casting;

    void Start()
    {
        _castTime = Time.time + CastDelay;
        _casting = false;
    }

    public void SetNode(WalkableNode node) {
        NodeLocation = node;
    }

    public bool IsCasting() {
        return _casting;
    }

    private void Update() {
        if (!_casting && _castTime < Time.time) {
            _casting = true;
            EventManager.GetInstance().InvokeCharmEvent(NodeLocation);
        }

    }

    public void Die() {
        EventManager.GetInstance().InvokeWitchDespawnEvent();
        Destroy(gameObject);
    }
    private void Consume(GameObject person) {
        person.GetComponent<PersonBehaviour>().Die();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Person")) {
            Consume(other.gameObject);
        }
    }

}
