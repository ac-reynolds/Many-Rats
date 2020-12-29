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
    private Vector2 targetOfMovementDirection;
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
            targetOfMovementDirection = witchObject.transform.position;
            movementSpeed = walkingSpeed;
        }
        else
        {
            Debug.Log("running");
            targetOfMovementDirection = CalculateAverageRatVector(nearbyRats);
            movementSpeed = runningSpeed;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetOfMovementDirection, walkingSpeed * Time.deltaTime);
    }

    //finds the direction of the sum of rat -> person vectors
    private Vector3 CalculateAverageRatVector(List<GameObject> ratsRatsRats)
    {
        Vector3 sumVector = Vector3.zero;
        foreach (GameObject rat in ratsRatsRats) {
            sumVector += transform.position - rat.transform.position;
        }
        return sumVector.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("RatHorde") || other.CompareTag("Witch"))
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
