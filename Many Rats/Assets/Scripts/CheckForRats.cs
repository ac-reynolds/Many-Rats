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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            otherRats.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            otherRats.Remove(other.gameObject);
        }
    }
}
