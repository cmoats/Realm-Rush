using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    [SerializeField] Tower towers;

    Vector2Int coordinates;
    GridManager gridManager;
    PathFinder pathfinder;


    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        if(gridManager != null)
        {
            
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
        
    }
    private void OnMouseDown()
    {
        
        if (gridManager.GetNode(coordinates).isWalkable && pathfinder.WillBlockPath(coordinates) != true)
        {
            bool isPlaced = towers.CreateTower(towers, transform.position);
            Debug.Log("is Placed " +isPlaced);

            if (isPlaced)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.notifyReceivers();
                isPlaced = false;
            }
            
        }
        
    }
}
