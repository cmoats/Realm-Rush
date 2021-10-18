using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{

    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int endCoordinates;

    Node currentSearchNode;
    Node startNode;
    Node endNode;

    Queue<Node> frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    

    public Vector2Int startcoordinates { get { return startCoordinates; } }
    public Vector2Int destinationcoordinates { get { return endCoordinates; } }



    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if(gridManager != null)
        {
            grid = gridManager.Grid;
            startNode = gridManager.Grid[startCoordinates];
            endNode = gridManager.Grid[endCoordinates];
            

        }

        
    }
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(startcoordinates);
    }
    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNode();
        BreadthFirstSearch(coordinates);
        
        return PrintPath();
    }

    void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int SurroundingTile = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(SurroundingTile))
            {
                neighbors.Add(grid[SurroundingTile]);
            }
           
        }

        foreach(Node neighbor in neighbors)
        {
            if(!reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                reached.Add(neighbor.coordinates, neighbor);
                frontier.Enqueue(neighbor);
                neighbor.connectedTo = currentSearchNode;
            }
        }
    }
    
    void BreadthFirstSearch(Vector2Int coordinates)
    {

        startNode.isWalkable = true;
        endNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();

        bool isRunning = true;
        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);

        while(frontier.Count > 0 && isRunning)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if(currentSearchNode.coordinates == endCoordinates)
            {
                isRunning = false;
            }
        }
    }

    
    List<Node> PrintPath()
    {
        List<Node> ourpath = new List<Node>();
        ourpath.Clear();

        Node currentNode = endNode;
        currentNode.isPath = true;
        ourpath.Add(currentNode);

        while (currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            ourpath.Add(currentNode);
            currentNode.isPath = true;
            
        }
        ourpath.Reverse();

               

        return ourpath;
    }


    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

            
        }
        return false;
    } 

    public void notifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }
}
