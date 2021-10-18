using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
[ExecuteAlways]
public class Coordinates : MonoBehaviour
{

    [SerializeField] Color ourColor = Color.green;
    [SerializeField] Color BlockedColor = Color.black;
    [SerializeField] Color ExploredColor = Color.red;
    [SerializeField] Color PathColor = Color.blue;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;


    
    // Start is called before the first frame update
    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            ChangeName();
            
        }
        ColorChange();
        ToggleLabels();
    }

    void ColorChange()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            label.color = BlockedColor;
        }

        else if (node.isPath) 
        {
            label.color = PathColor;
        }
        else if (node.isExplored)
        {
            label.color = ExploredColor;
        }
        else
        {
            label.color = ourColor;
        }
    }

    void ToggleLabels()
    {
        
       
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
            
        }
    }

    void DisplayCoordinates()
    {
        if(gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/gridManager.unityGridSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/ gridManager.unityGridSize);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void ChangeName()
    {
        transform.parent.name = coordinates.ToString();
        
    }
}
