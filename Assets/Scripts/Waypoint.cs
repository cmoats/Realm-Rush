using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    [SerializeField] Tower towers;

    private void OnMouseDown()
    {

        if (isPlaceable)
        {
            bool isPlaced = towers.CreateTower(towers, transform.position);
            isPlaceable = !isPlaced;
        }
        
    }
}
