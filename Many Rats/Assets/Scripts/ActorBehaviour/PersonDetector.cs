using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDetector : MonoBehaviour
{   
    public RatHordeBehaviour ratirl { get; set; }

    // add person in range to list of peopoleFound
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Person"))
        {
            ratirl.Kill(other.GetComponent<PersonBehaviour>());
            Debug.Log("KILL KILL KILL");
        }
    }
}
