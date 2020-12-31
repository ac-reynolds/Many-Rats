using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarriageBehaviour : MonoBehaviour
{
    public WalkableNode NodeLocation
    {
        get; set;
    }

    public void Die() {
        Destroy(gameObject);
    }
    private void AcceptPassenger(GameObject person) {
        person.GetComponent<PersonBehaviour>().Die();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Person")) {
            AcceptPassenger(other.gameObject);
        }
    }
}
