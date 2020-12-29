using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PersonBehaviour : MonoBehaviour
{
    [SerializeField] private float ratCheckRadius;
    [SerializeField] private CircleCollider2D ratCheckerCollider;
    [SerializeField] private GameObject ratCheckerObject;
    private List<GameObject> nearbyRats;
    private CheckForRats checkForRats;
    private GameObject witchObject;
    private Vector2 movementDirection;
    private float movementSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;

    public UnityEvent personDelivered;

    // Start is called before the first frame update
    void Start()
    {
        ratCheckerCollider.radius = ratCheckRadius;
        checkForRats = ratCheckerObject.GetComponent<CheckForRats>();
    }

    // Update is called once per frame
    void Update()
    {
        nearbyRats = checkForRats.ReturnRats();
        if (nearbyRats.Count > 0)
        {
            CalculateAverageRatVector();
            // movementDirection = -averageRatVector;
            movementSpeed = runningSpeed;
        }
        else
        {
            witchObject = GameObject.FindGameObjectWithTag("Witch");
            movementDirection = witchObject.transform.position - this.transform.position;
            movementSpeed = walkingSpeed;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, movementDirection, walkingSpeed);
    }

    void CalculateAverageRatVector()
    {
        // do adam math things here
        // List of nearby Rat gameObjects is in 'nearbyRats'
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RatHorde") || other.CompareTag("Witch"))
        {
            Destroy(this);
        }
        if(other.CompareTag("Carriage"))
        {
            personDelivered.Invoke();
            Destroy(this);
        }
    }
}
