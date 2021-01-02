using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatDetector : MonoBehaviour
{
    private float _sqTriggerDistance;

    private void Start()
    {
        _sqTriggerDistance = Mathf.Pow(GetComponent<CircleCollider2D>().radius, 2);
    }

    List<GameObject> GetNearbyRats() {
        List<GameObject> nearbyRats = new List<GameObject>();
        GameObject[] activeRats = GameObject.FindGameObjectsWithTag("Rat");
        foreach (GameObject rat in activeRats) {
            if (Vector2.SqrMagnitude(transform.position - rat.transform.position) < _sqTriggerDistance) {
                nearbyRats.Add(rat);
            }
        }
        return nearbyRats;
    }

    List<GameObject> GetNearbyRatHordes() {
        List<GameObject> nearbyRatHordes = new List<GameObject>();
        GameObject[] activeRatHordes = GameObject.FindGameObjectsWithTag("RatHorde");
        foreach (GameObject ratHorde in activeRatHordes) {
            if (Vector2.SqrMagnitude(transform.position - ratHorde.transform.position) < _sqTriggerDistance) {
                nearbyRatHordes.Add(ratHorde);
            }
        }
        return nearbyRatHordes;
    }

    /*
     * Returns a normalized vector pointing away from nearby rats
     */
    public Vector2 RunningFromRatsDirection() {
        Vector2 fleeDirection = new Vector2();
        foreach (GameObject rat in GetNearbyRats()) {
            if (rat != null) {
                Vector2 ratToObject = (Vector2)(transform.position - rat.transform.position);
                fleeDirection += ratToObject.normalized;
            }
        }
        foreach (GameObject horde in GetNearbyRatHordes()) {
            if(horde != null) {
                Vector2 hordeToObject = (Vector2)(transform.position - horde.transform.position);
                fleeDirection += hordeToObject.normalized * (_sqTriggerDistance - Vector2.SqrMagnitude(hordeToObject));
            }
        }
        return fleeDirection.normalized;
    }

    public int NumNearbyRats() {
        return GetNearbyRats().Count;
    }

    public List<GameObject> NearbyRats() {
        return GetNearbyRats();
    }

    public void Die() {
    }
}
