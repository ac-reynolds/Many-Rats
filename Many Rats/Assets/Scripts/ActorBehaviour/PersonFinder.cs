using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFinder : MonoBehaviour
{
    private List<GameObject> peopleFound;

    private void Start()
    {
        peopleFound = new List<GameObject>();
    }

    public List<GameObject> ReturnPeople()
    {
        return peopleFound;
    }

    // add person in range to list of peopoleFound
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Person"))
        {
            peopleFound.Add(other.gameObject);
        }
    }

    // remove person no longer in range from list of peopleFound
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Person"))
        {
            peopleFound.Remove(other.gameObject);
        }
    }
}
