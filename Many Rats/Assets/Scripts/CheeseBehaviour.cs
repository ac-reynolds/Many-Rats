using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBehaviour : MonoBehaviour
{
    [SerializeField] private float cheeseDespawnTime;
    private float spawnTime;

    // log the time the cheese spawned
    void Awake()
    {
        spawnTime = Time.time;
    }

    // despawn the cheese after cheeseDespawnTime seconds have passed
    void Update()
    {
        if (Time.time - cheeseDespawnTime > spawnTime)
            this.gameObject.SetActive(false);
    }

    // destroy any rats who touch the cheese
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Rat"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
