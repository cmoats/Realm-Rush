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

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;


    
    // Start is called before the first frame update
    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates();
        waypoint = GetComponentInParent<Waypoint>();
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
        if(!waypoint.IsPlaceable)
        {
            label.color = BlockedColor;
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x/UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z/ UnityEditor.EditorSnapSettings.move.z);
        label.text = coordinates.x + "," + coordinates.y;
    }

    void ChangeName()
    {
        transform.parent.name = coordinates.ToString();
        
    }
}
