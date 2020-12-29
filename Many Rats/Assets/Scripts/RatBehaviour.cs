using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBehaviour : MonoBehaviour
{
    public float RatRandomness = 10;
    public float RatSpeed = 1;

    private const int MAX_DEGREES = 360;
    private Quaternion _displacement;

    // rat horde stuff
    [SerializeField] private float ratCheckRadius;
    [SerializeField] private CircleCollider2D ratCheckerCollider;
    [SerializeField] private GameObject ratCheckerObject;
    private CheckForRats checkForRats;
    private List<GameObject> nearbyRats;
    [SerializeField] private int ratHordeCriticalMass;
    [SerializeField] private GameObject ratHordePrefab;

    // cheese stuff
    [SerializeField] private float cheeseCheckRadius;
    [SerializeField] private CircleCollider2D cheeseFinderCollider;
    [SerializeField] private GameObject cheeseFinderObject;
    private CheeseFinder cheeseFinder;
    private bool nearbyCheese;

    void Start()
    {
        _displacement = Quaternion.AngleAxis(Random.Range(0, MAX_DEGREES), Vector3.forward);

        // find rat checker
        ratCheckerCollider.radius = ratCheckRadius;
        checkForRats = ratCheckerObject.GetComponent<CheckForRats>();

        // find cheese finder
        cheeseFinderCollider.radius = cheeseCheckRadius;
        cheeseFinder = cheeseFinderObject.GetComponent<CheeseFinder>();

        nearbyCheese = false;
    }
    private void Update()
    {
        // check # of nearby rats
        nearbyRats = checkForRats.ReturnRats();
        if (nearbyRats != null)
        {
            // if there's enough to form a horde
            if (nearbyRats.Count > ratHordeCriticalMass)
            {
                for(int i = 0; i < nearbyRats.Count; i++)
                {
                    // set other rats inactive
                    nearbyRats[i].SetActive(false);
                }
                // instantiate the horde, then set this rat as inactive
                Instantiate(ratHordePrefab, transform.position, Quaternion.identity);
                this.gameObject.SetActive(false);
            }
        }
        

    }
    void FixedUpdate()
    {
        // check if cheese has been found
        nearbyCheese = cheeseFinder.ReturnCheeseFound();
        if (nearbyCheese == true)
        {
            // move directly towards cheese if there is cheese detected
            transform.position = Vector2.MoveTowards(transform.position, cheeseFinder.ReturnCheeseCoordinates(), RatSpeed * Time.deltaTime);
        }
        else
        {
            //rotate in a random direction
            _displacement *= Quaternion.AngleAxis(Random.Range(-RatRandomness, RatRandomness), Vector3.forward);
            Vector3 dx = _displacement * Vector3.right * Time.deltaTime * RatSpeed;
            Vector3.MoveTowards(transform.position, transform.position + dx, RatSpeed * Time.deltaTime);
            transform.position = transform.position + dx;

            //flip orientation based on motion
            Vector3 scale = transform.localScale;
            scale.x = dx.x > 0 ? 1 : -1;
            transform.localScale = scale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //avoid other rats
        if (collider.CompareTag("Rat"))
        {
            Vector3 newDirection = transform.position - collider.transform.position;
            _displacement.SetFromToRotation(Vector2.right, newDirection);
        }
    }
}
