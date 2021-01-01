using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDetector : MonoBehaviour
{
    private List<GameObject> _nearbyRats;
    private List<GameObject> _nearbyRatHordes;
    private float _sqTriggerDistance;

    private void Start()
    {
        _nearbyRats = new List<GameObject>();
        _nearbyRatHordes = new List<GameObject>();
        _sqTriggerDistance = Mathf.Pow(GetComponent<CircleCollider2D>().radius, 2);
        EventManagerOneArg<SpawnRatEvent, GameObject>.GetInstance().AddListener(OnRatSpawn);
        EventManagerOneArg<SpawnRatHordeEvent, GameObject>.GetInstance().AddListener(OnRatHordeSpawn);
        EventManagerOneArg<DespawnRatEvent, GameObject>.GetInstance().AddListener(OnRatDespawn);
    }

    /*
     * Returns a normalized vector pointing away from nearby rats
     */
    public Vector2 RunningFromRatsDirection() {
        Vector2 fleeDirection = new Vector2();
        foreach (GameObject rat in _nearbyRats) {
            Vector2 ratToObject = (Vector2)(transform.position - rat.transform.position);
            fleeDirection += ratToObject.normalized * (_sqTriggerDistance - Vector2.SqrMagnitude(ratToObject));
        }
        foreach (GameObject horde in _nearbyRatHordes) {
            Vector2 hordeToObject = (Vector2)(transform.position - horde.transform.position);
            fleeDirection += hordeToObject.normalized * (_sqTriggerDistance - Vector2.SqrMagnitude(hordeToObject));
        }
        return fleeDirection.normalized;
    }

    private void OnRatDespawn(GameObject rat) {
        if (Vector3.SqrMagnitude(rat.transform.position - transform.position) < _sqTriggerDistance) {
            _nearbyRats.Remove(rat);
        }
    }

    private void OnRatSpawn(GameObject rat) {
        if (Vector3.SqrMagnitude(rat.transform.position - transform.position) < _sqTriggerDistance) {
            _nearbyRats.Add(rat);
        } 
    }

    private void OnRatHordeSpawn(GameObject ratHorde) {
        if (Vector3.SqrMagnitude(ratHorde.transform.position - transform.position) < _sqTriggerDistance) {
            _nearbyRats.Add(ratHorde);
        }
    }

    public int NumNearbyRats() {
        return _nearbyRats.Count;
    }

    public List<GameObject> NearbyRats() {
        return _nearbyRats;
    }

    // adds rat in range to list of otherRats
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rat")) {
            _nearbyRats.Add(other.gameObject);
        }
        else if (other.CompareTag("RatHorde")) {
            _nearbyRatHordes.Add(other.gameObject);
        }
    }

    // removes rat no longer in range from list of otherRats
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            _nearbyRats.Remove(other.gameObject);
        }
        else if (other.CompareTag("RatHorde")) {
            _nearbyRatHordes.Remove(other.gameObject);
        }
    }

    public void Die() {
        EventManagerOneArg<SpawnRatEvent, GameObject>.GetInstance().RemoveListener(OnRatSpawn);
        EventManagerOneArg<SpawnRatHordeEvent, GameObject>.GetInstance().RemoveListener(OnRatHordeSpawn);
        EventManagerOneArg<DespawnRatEvent, GameObject>.GetInstance().RemoveListener(OnRatDespawn);
    }
}
