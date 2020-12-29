using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForRats : MonoBehaviour
{
    private List<GameObject> otherRats;

    public List<GameObject> ReturnRats()
    {
        return otherRats;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            otherRats.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            otherRats.Remove(other.gameObject);
        }
    }
}
