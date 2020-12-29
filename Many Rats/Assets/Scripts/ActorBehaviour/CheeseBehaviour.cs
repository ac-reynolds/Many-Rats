using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseBehaviour : MonoBehaviour
{
    [SerializeField] private float cheeseDespawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > cheeseDespawnTime)
            this.gameObject.SetActive(false);
    }
}
