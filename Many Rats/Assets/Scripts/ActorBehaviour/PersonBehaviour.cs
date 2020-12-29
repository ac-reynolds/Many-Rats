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

    void Start()
    {
        //ratCheckerCollider.radius = ratCheckRadius;
        //checkForRats = ratCheckerObject.GetComponent<CheckForRats>();
        movementDirection = transform.position;
    }

    public void OnCharm(WalkableNode location) {
        Debug.Log("accepted");
    }

    void Update()
    {
        return;
        // check for nearby rats
        nearbyRats = checkForRats.ReturnRats();
        if (nearbyRats.Count > 0)
        {
            // if there are any nearby rats, calculate average rat position and change to running speed
            //Debug.Log("running");
            CalculateAverageRatVector();
            movementDirection = Vector3.Normalize(CalculateAverageRatVector());
            movementSpeed = runningSpeed;
        }
        else
        {
            // otherwise find the witch and move directly towards witch and change to wakling speed
            //Debug.Log("moving");
            witchObject = GameObject.FindGameObjectWithTag("Witch");
            if (witchObject != null)
            {
                movementDirection = Vector3.Normalize(witchObject.transform.position);
            }
            movementSpeed = walkingSpeed;
        }
    }

    private void FixedUpdate()
    {
        // move towards movementDirection
        transform.position = Vector2.MoveTowards(transform.position, movementDirection, movementSpeed * Time.deltaTime);
    }

    // calculate avg vector away from all nearby rats
    private Vector3 CalculateAverageRatVector()
    {
        Vector3 averageRatVector = new Vector3(0, 0, 0);
        foreach(GameObject rat in nearbyRats)
        {
            averageRatVector += transform.position - rat.transform.position;
        }
        return averageRatVector;
    }

    // collision cases for different objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("RatHorde"))
        {
            this.gameObject.SetActive(false);
            other.gameObject.SetActive(false);
        }
        if(other.CompareTag("Witch"))
        {
            this.gameObject.SetActive(false);
        }
        if(other.CompareTag("Carriage"))
        {
            personDelivered.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
