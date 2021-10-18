using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour


{
    [SerializeField] Vector2Int gridsize;

    [Tooltip("Should match unity Grid Size snap settings")]
    [SerializeField] int WorldGridSize =10;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    public int unityGridSize { get { return WorldGridSize; } }
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }

    private void Awake()
    {
        CreateGrid();
       
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }

        return null;
    }

    private void CreateGrid()
    {
        for(int x = 0; x <= gridsize.x; x++)
        {
            for(int y = 0; y <= gridsize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
                
            }
        }
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNode()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / WorldGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / WorldGridSize);

        return coordinates;
        
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * WorldGridSize;
        position.z = coordinates.y * WorldGridSize;
        position.y = 0;

        return position;
    }
}
