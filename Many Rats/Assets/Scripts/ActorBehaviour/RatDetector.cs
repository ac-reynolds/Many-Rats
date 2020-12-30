using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDetector : MonoBehaviour
{
    private List<GameObject> _nearbyRats;
    private float _sqTriggerDistance;

    private void Start()
    {
        _nearbyRats = new List<GameObject>();
        _sqTriggerDistance = Mathf.Pow(GetComponent<CircleCollider2D>().radius, 2);
        EventManager.GetInstance().RegisterRatSpawnedEvent(OnRatSpawn);
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
        return fleeDirection.normalized;
    }

    private void OnRatSpawn(GameObject rat) {
        if (Vector3.SqrMagnitude(rat.transform.position - transform.position) < _sqTriggerDistance) {
            _nearbyRats.Add(rat);
        } 
    }

    public bool RatsNearby() {
        return (_nearbyRats.Count > 0);
    }

    // adds rat in range to list of otherRats
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            _nearbyRats.Add(other.gameObject);
        }
    }

    // removes rat no longer in range from list of otherRats
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            _nearbyRats.Remove(other.gameObject);
        }
    }

    public void Die() {
        EventManager.GetInstance().UnregisterRatSpawnedEvent(OnRatSpawn);
    }
}
