using UnityEngine;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour
{
    [SerializeField]
    private PathfindingGridManager grid;

    private List<Node> path;

    public List<Node> GetPath()
    {
        return path;
    }

    void Start()
    {
        path = new List<Node>();
    }

    public void FindPathToTarget(Vector3 startPosition, Vector3 targetPosition)
    {
        //ensures the target and start are within bounds
        //otherwise path finding gets very unhappy.
        if (grid.CheckBounds(startPosition) && grid.CheckBounds(targetPosition))
        {
            //find the path
            FindPath(startPosition, targetPosition); 

            //debug draw the line.
            Color colour = Color.red;
            //for each node, draw a line to the next node
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(new Vector3(path[i].GetPosition()[0], path[i].GetPosition()[1], 0), new Vector3(path[i + 1].GetPosition()[0], path[i + 1].GetPosition()[1], 0), colour, Time.fixedDeltaTime);
            }

        }
        else
        {
            path.Clear();
        }
    }

    //uses A* pathfinding to search for the player
    private void FindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        //get the start and target nodes
        Node startNode = grid.GetNodeFromPosition(startPosition);
        Node targetNode = grid.GetNodeFromPosition(targetPosition);

        //initialise the open and closed lists for nodes
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        //adds the first list to the node.
        openList.Add(startNode);

        //while there are still nodes that haven't been explored.
        while (openList.Count > 0)
        {
            //current node is the first node in the open set.
            Node currentNode = openList[0];
            //check the rest of the list to see if there is a better node. if open list has only 1 node, for loop is skipped.
            for (int i = 1; i < openList.Count; i++)
            {
                //if the node from the list has a better F Cost
                if (openList[i].GetFCost() < currentNode.GetFCost())
                {
                    //update current node
                    currentNode = openList[i];
                }
                //if the node from the list has an equal F Cost
                else if (openList[i].GetFCost() == currentNode.GetFCost())
                {
                    //if the node has a better H Cost
                    if (openList[i].GetHCost() < currentNode.GetHCost())
                    {
                        //update current node
                        currentNode = openList[i];
                    }
                }
            }

            //if current node is the target node
            if (currentNode == targetNode)
            {
                //path has been found.
                //Debug.Log("Path has been found.");
                CreatePath(startNode, targetNode);
                return;
            }

            //remove current node from open list, put in closed list
            //closed list is for nodes that have been looked at 
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            int costToNode;

            foreach (Node node in grid.GetNeighbours(currentNode))
            {
                // if it's not in the closed list
                if (!closedList.Contains(node))
                {
                    //calculate the gcost to the neighbour, via the current node
                    //(distance between nodes is always 1)
                    costToNode = currentNode.GetGCost() + 1;
                    //if node not in open list
                    if (!openList.Contains(node))
                    {
                        //set the g cost
                        node.SetGCost(costToNode);
                        //calculate the h cost
                        node.SetHCost(GetHCost(node, targetNode));
                        //set the parent
                        node.SetParent(currentNode);
                        //add to the open list
                        openList.Add(node);
                    }
                    //if node is in open list, and route to it is better
                    else if (costToNode < node.GetGCost())
                    {
                        //update the g cost
                        node.SetGCost(costToNode);
                        //update the parent
                        node.SetParent(currentNode);
                    }
                }
            }

        }
        //Debug.Log("Path NOT found :(");
    }

    private void CreatePath(Node startNode, Node targetNode)
    {
        //clear the previous path
        path.Clear();
        //create a temporary node
        Node tempNode = targetNode;
        //while the start node hasn't been reached
        //iterate through each node's parent, and add it to the list
        while (tempNode != startNode)
        {
            tempNode = tempNode.GetParent();
            path.Add(tempNode);
        }
        //list is reversed so that the first node is the next node the enemy needs to go to.
        path.Reverse();

    }

    //gets the hCost of a given node, using manhattan method.
    private int GetHCost(Node currentNode, Node targetNode)
    {
        //get the positions of the nodes
        int[] currentPosition = currentNode.GetPosition();
        int[] targetPosition = targetNode.GetPosition();

        //find the distance between the x and y coordinates of the nodes
        int xDistance = Mathf.Abs(currentPosition[0] - targetPosition[0]);
        int yDistance = Mathf.Abs(currentPosition[1] - targetPosition[1]);

        //h cost is total of two distances.
        return xDistance + yDistance;
    }

}
