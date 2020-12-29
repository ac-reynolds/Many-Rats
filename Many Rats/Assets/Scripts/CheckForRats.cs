using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rat"))
        {
            //Debug.Log("adding rat");
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
