using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForRats : MonoBehaviour
{
    private List<GameObject> otherRats;

    private void Start()
    {
        otherRats = new List<GameObject>();
    }

    public List<GameObject> ReturnRats()
    {
        return otherRats;
    }

    // adds rat in range to list of otherRats
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            otherRats.Add(other.gameObject);
        }
    }

    // removes rat no longer in range from list of otherRats
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            otherRats.Remove(other.gameObject);
        }
    }
}
