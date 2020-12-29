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

    void Start()
    {
        // assume closest person is unreasonably far away, so even actually far away people will still be chased
        closestPersonVector = new Vector3(10000, 10000, 10000);
        personFinderCollider.radius = personFindRadius;
        personFinder = personFinderObject.GetComponent<PersonFinder>();
        currentTarget = transform.position;
    }

    void Update()
    {
        // check if there's any persons nearby
        peopleFound = personFinder.ReturnPeople();
        if (peopleFound != null)
        {
            for (int i = 0; i < peopleFound.Count; i++)
            {
                // check who is the closest
                if ((peopleFound[i].transform.position - transform.position).magnitude < closestPersonVector.magnitude)
                {
                    closestPersonVector = peopleFound[i].transform.position - transform.position;
                    closestPersonIndex = i;
                }
            }
            // get their position
            currentTarget = peopleFound[closestPersonIndex].transform.position;
        }
    }

    private void FixedUpdate()
    {
        // hunt them down
        transform.position = Vector2.MoveTowards(transform.position, currentTarget, ratHordeSpeed * Time.deltaTime);
    }
}
