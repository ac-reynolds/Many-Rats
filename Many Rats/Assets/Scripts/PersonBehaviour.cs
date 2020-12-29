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
        if (nearbyRats == null)
        {
            //Debug.Log("moving");
            witchObject = GameObject.FindGameObjectWithTag("Witch");
            movementDirection = witchObject.transform.position;
            movementSpeed = walkingSpeed;
        }
        else
        {
            //Debug.Log("running");
            CalculateAverageRatVector();
            // movementDirection = -averageRatVector;
            movementSpeed = runningSpeed;
        }
    }

    private void FixedUpdate()
    {
<<<<<<< Updated upstream
        transform.position = Vector2.MoveTowards(transform.position, movementDirection, walkingSpeed);
=======
        //transform.position = Vector2.MoveTowards(transform.position, movementDirection, walkingSpeed * Time.deltaTime);
>>>>>>> Stashed changes
    }

    void CalculateAverageRatVector()
    {
        // do adam math things here
        // List of nearby Rat gameObjects is in 'nearbyRats'
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("RatHorde") || other.CompareTag("Witch"))
        {
            
        }
        if(other.CompareTag("Carriage"))
        {
            personDelivered.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
