using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonFinder : MonoBehaviour
{
    private List<GameObject> peopleFound;

<<<<<<< Updated upstream
    private void Start()
    {
        peopleFound = new List<GameObject>();
    }
=======
>>>>>>> Stashed changes
    public List<GameObject> ReturnPeople()
    {
        return peopleFound;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Person"))
        {
            peopleFound.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Person"))
        {
            peopleFound.Remove(other.gameObject);
        }
    }
}
