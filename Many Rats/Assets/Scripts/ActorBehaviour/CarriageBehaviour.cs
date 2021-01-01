using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CarriageBehaviour : MonoBehaviour
{
    public UnityEvent onPersonDespawn;

    private void Start() {
        EventManagerOneArg<SpawnCarriageEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
    }
    public WalkableNode NodeLocation
    {
        get; set;
    }

    private void AcceptPassenger(GameObject person) {
        EventManagerZeroArgs<CarriageLoadingSuccessfulEvent>.GetInstance().InvokeEvent();
        onPersonDespawn.Invoke();
        person.GetComponent<PersonBehaviour>().Die();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Person")) {
            AcceptPassenger(other.gameObject);
        }
    }
    public void Die() {
        EventManagerOneArg<DespawnCarriageEvent, GameObject>.GetInstance().InvokeEvent(gameObject);
        Destroy(gameObject);
    }
}
