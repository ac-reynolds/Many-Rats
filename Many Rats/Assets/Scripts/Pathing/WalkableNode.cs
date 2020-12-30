using System;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

/*
 * Node class for specifying walkable graphs.  
 */
[Serializable]
public class WalkableNode : MonoBehaviour
{
    public List<WalkableNode> neighbors;
    private List<float> _nodeDistances;//distances in same order as neighbors

    //for easier editing
    private void OnDrawGizmos() {
        foreach (WalkableNode n in neighbors) {
            Gizmos.DrawLine(transform.position, n.transform.position);
        }
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        Handles.Label(transform.position, name, style);
    }

    void Awake () {
        //update neighbor list of other nodes, for consistency
        foreach (WalkableNode n in neighbors) {
            if (!n.neighbors.Contains(this)) {
                n.neighbors.Add(this);
            }
        }
    }

    void Start()
    {
        UpdateNeighborDistances();
    }

    private void UpdateNeighborDistances() {
        if (_nodeDistances == null) {
            _nodeDistances = new List<float>();
            for (int i = 0; i < neighbors.Count; i++) {
                float distToNeighbor = Vector2.Distance(transform.position, neighbors[i].transform.position);
                _nodeDistances.Add(distToNeighbor);
            }
        }
    }

    //retrieves the distance to another node, using the stored distance values calculated at start when possible
    public float DistanceToNode(WalkableNode n) {

        if(neighbors.Contains(n)) {
            return _nodeDistances[neighbors.IndexOf(n)];
        } else {
        
            return Vector3.Distance(transform.position, n.transform.position);
        
        }
    }

    /*
     * Dijkstra greedy algorithm to find shortest path to target.  Returns a list of node to be visited from this node, including this node.
     */
    public List<WalkableNode> RouteToTarget(WalkableNode target) {

        //keys are nodes, values contain min distance to each node and previous node to get that min distance
        Dictionary<WalkableNode, Tuple<float, WalkableNode>> visitedNodes = new Dictionary<WalkableNode, Tuple<float, WalkableNode>>();

        //keys are nodes, values are previous node (connection to visited node mass)
        Dictionary<WalkableNode, WalkableNode> prospectiveNodes = new Dictionary<WalkableNode, WalkableNode>();

        //intialize dictionaries
        visitedNodes[this] = new Tuple<float, WalkableNode>(0.0f, null);
        foreach (WalkableNode n in neighbors) {
            prospectiveNodes[n] = this;
        }

        //recover path from visited dictionary
        List<WalkableNode> path = new List<WalkableNode>();

        if (SummonDijkstra(ref visitedNodes, ref prospectiveNodes, target)) {
            foreach (KeyValuePair<WalkableNode, Tuple<float, WalkableNode>> entry in visitedNodes) {
                //Debug.Log(entry.Key.name);
            }
            WalkableNode reversePathNode = target;
            while (reversePathNode != this) {
                path.Add(reversePathNode);
                //Debug.Log(reversePathNode.name);
                reversePathNode = visitedNodes[reversePathNode].Item2;
            }
            path.Add(this);
            path.Reverse();
        }

        return path; 
    }
    private bool SummonDijkstra(ref Dictionary<WalkableNode, Tuple<float, WalkableNode>> visited, 
                    ref Dictionary<WalkableNode, WalkableNode> prospective, WalkableNode target) {
        
        bool pathFound = false;

        while (prospective.Count > 0) {

            //search through prospective nodes to find next node
            WalkableNode nextNode = null;
            float nextNodeDistance = float.MaxValue;
            foreach (KeyValuePair<WalkableNode, WalkableNode> entry in prospective) {
                //entry.Key is new prospective node, entry.Value is visited node it's connected to
                float newNodeDistance = visited[entry.Value].Item1 + entry.Key.DistanceToNode(entry.Value);
                if (newNodeDistance < nextNodeDistance) {
                    nextNode = entry.Key;
                    nextNodeDistance = newNodeDistance;
                }
            }

            //Debug.Log("-------------------");
            //Debug.Log("visiting " + nextNode.name + " at distance " + nextNodeDistance);
            //PrintD1(visited);
            //  PrintD2(prospective);

            //otherwise, add our node to visited with distance information and purge it from prospective nodes
            visited.Add(nextNode, new Tuple<float, WalkableNode>(nextNodeDistance, prospective[nextNode]));
            prospective.Remove(nextNode);

            //target found!  hurray!
            if (target.Equals(nextNode)) {
                pathFound = true;
                break;
            }

            //add unvisited neighbors of next node to prospective nodes
            foreach (WalkableNode n in nextNode.neighbors) {
                //Debug.Log(nextNode.name + " has a neighbor " + n.name);
                if (!visited.ContainsKey(n) && !prospective.ContainsKey(n)) {
                    //Debug.Log("added " + n.name);
                    prospective[n] = nextNode;
                }
            }
        }

        return pathFound;
    }

    void PrintD1(Dictionary<WalkableNode, Tuple<float, WalkableNode>> v) {
        foreach (KeyValuePair<WalkableNode, Tuple<float, WalkableNode>> n in v) {
            Debug.Log(n.Key + " -[prev]-> " + n.Value.Item2 + " (" + n.Value.Item1 + ")");
        }
    }

    void PrintD2(Dictionary<WalkableNode, WalkableNode> p) {
        foreach (KeyValuePair<WalkableNode, WalkableNode> n in p) {
            Debug.Log(n.Key + " -[prev]-> " + n.Value);
        }
    }

    public override String ToString() {
        return gameObject.name;
    }
}
