using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseFinder : MonoBehaviour
{
    private Vector3 foundCoordinates;
    private bool foundCheese = false;

    // if CheeseFinder collides with cheese...
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cheese"))
        {
            foundCoordinates = other.transform.position;
            foundCheese = true;
        }
    }

    // tell rat that cheese has been found
    public bool ReturnCheeseFound()
    {
        return foundCheese;
    }

    // also tell rat where found cheese is
    public Vector3 ReturnCheeseCoordinates()
    {
        return foundCoordinates;
    }
}
