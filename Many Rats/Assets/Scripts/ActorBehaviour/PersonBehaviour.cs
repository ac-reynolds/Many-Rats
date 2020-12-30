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


    public float WaypointTagDistance = .5f;


    private GameObject[] _allNodes;//for finding nearest node
    private bool _isCharmed = false;
    private WalkableNode _charmSource;
    private List<WalkableNode> _pathToWitch;
    private int _nextNodeOnPath;
    private Vector2 _direction;


    //do not call OnCharm during start ! waypoints aren't ready to go yet at start and we could fix that
    //but for now just do not do it!
    void Start()
    {
        EventManager.GetInstance().RegisterCharmEvent(OnCharm);
        _allNodes = GameObject.FindGameObjectsWithTag("Waypoint");
        movementSpeed = walkingSpeed;
        //ratCheckerCollider.radius = ratCheckRadius;
        //checkForRats = ratCheckerObject.GetComponent<CheckForRats>();
    }

    void Update()
    {

        //check for witch present if uncharmed
        GameObject witch = GameObject.FindWithTag("Witch");
        if (!_isCharmed && witch != null) {
            WitchBehaviour wb = witch.GetComponent<WitchBehaviour>();
            if (wb.IsCasting()) {
                Debug.Log("being charmed ! ");
                OnCharm(wb.NodeLocation);
            }
        }

        if(_isCharmed) {
            WalkableNode nextWaypoint = _pathToWitch[_nextNodeOnPath];

            //if we're close enough to a waypoint, we'll move to the next one
            if (Vector2.SqrMagnitude(transform.position - nextWaypoint.transform.position) < WaypointTagDistance
                            && _nextNodeOnPath < _pathToWitch.Count - 1) {
                _nextNodeOnPath++;
                nextWaypoint = _pathToWitch[_nextNodeOnPath];
            }
            _direction = (nextWaypoint.transform.position - transform.position).normalized;
        }

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
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + _direction, movementSpeed * Time.deltaTime);
    }
    private void OnCharm(WalkableNode location) {

        _isCharmed = true;
        _charmSource = location;

        //find closest waypoint to person
        WalkableNode closestNode = null;
        float minSquareDistance = float.MaxValue;
        foreach (GameObject node in _allNodes) {
            float nodeSquareDistance = Vector2.SqrMagnitude(transform.position - node.transform.position);
            if (nodeSquareDistance < minSquareDistance) {
                minSquareDistance = nodeSquareDistance;
                closestNode = node.GetComponent<WalkableNode>();
            }
        }

        _pathToWitch = closestNode.RouteToTarget(_charmSource);
        _nextNodeOnPath = 0;

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
