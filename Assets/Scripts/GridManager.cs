using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour


{
    [SerializeField] Vector2Int gridsize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

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

    void CreateGrid()
    {
        for(int x = 0; x<gridsize.x; x++)
        {
            for(int y = 0; y < gridsize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
                Debug.Log(grid[coordinates].coordinates);
            }
        }
    }
}
