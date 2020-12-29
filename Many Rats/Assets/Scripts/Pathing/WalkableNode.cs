using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Node class for specifying walkable graphs.  
 */
public class WalkableNode : MonoBehaviour
{

    public List<WalkableNode> neighbors;
    private List<float> _nodeDistances;

    void Awake () {
        //update neighbor list of other nodes, for consistency
        foreach (WalkableNode n in neighbors) {
            if (!n.neighbors.Contains(this)) {
                Debug.Log("not in nieghbor :(");
            }
        }
    }

    void Start()
    {
        //initialize graph with distances
        _nodeDistances = new List<float>();

        for (int i = 0; i < neighbors.Count; i++) {
            float distToNeighbor = Vector2.Distance(transform.position, neighbors[i].transform.position);
            _nodeDistances.Add(distToNeighbor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
