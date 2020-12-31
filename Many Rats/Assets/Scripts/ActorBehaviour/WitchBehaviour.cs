using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WitchBehaviour : MonoBehaviour
{
    [SerializeField] private float CastDelay = 1; //amount of time after spawning until the witch casts
    [SerializeField] private float _timeUntilDespawn = 20;

    private float _castTime;
    private bool _casting;
    private float _despawnTime;

    public WalkableNode NodeLocation
    {
        get; set;
    }

    void Start()
    {
        EventManagerOneArg<SpawnWitchEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
        _castTime = Time.time + CastDelay;
        _casting = false;
        _despawnTime = Time.time + _timeUntilDespawn;
    }

    public bool IsCasting() {
        return _casting;
    }

    private void Update() {
        if (!_casting && _castTime < Time.time) {
            _casting = true;
        }

        if (_despawnTime < Time.time) {
            Die();
        }

    }

    public void Die() {
        EventManagerOneArg<DespawnWitchEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
        Debug.Log("witch die");
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
