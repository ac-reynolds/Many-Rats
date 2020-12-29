using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHordeBehaviour : MonoBehaviour
{
    [SerializeField] private float personFindRadius;
    [SerializeField] private CircleCollider2D personFinderCollider;
    [SerializeField] private GameObject personFinderObject;
    private PersonFinder personFinder;
    private List<GameObject> peopleFound;
    private int closestPersonIndex;
    private Vector3 closestPersonVector;

    private Vector3 currentTarget;

    [SerializeField] private float ratHordeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        closestPersonVector = new Vector3(10000, 10000, 10000);
        personFinderCollider.radius = personFindRadius;
        personFinder = personFinderObject.GetComponent<PersonFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        peopleFound = personFinder.ReturnPeople();
        for(int i = 0; i < peopleFound.Count; i++)
        {
            if ((peopleFound[i].transform.position - transform.position).magnitude < closestPersonVector.magnitude)
            {
                closestPersonVector = peopleFound[i].transform.position - transform.position;
                closestPersonIndex = i;
            }
        }
        currentTarget = peopleFound[closestPersonIndex].transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, ratHordeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Person"))
        {
            Debug.Log("killed by " + other);
            other.transform.localScale *= 5;
            this.gameObject.SetActive(false);
        }
    }
}
