using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
     List<Node> path = new List<Node>();

    Enemy enemy;
    GridManager gridmanager;
    PathFinder pathfinder;
    private void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        gridmanager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<PathFinder>();
    }


    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
        
        
    }

    
    void RecalculatePath(bool resetpath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetpath)
        {
            coordinates = pathfinder.startcoordinates;
        }
        else
        {
            coordinates = gridmanager.GetCoordinatesFromPosition(transform.position);
            
        }

        
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
        Debug.Log("hi");
        
        
    }

    void ReturnToStart()
    {
        transform.position = gridmanager.GetPositionFromCoordinates(pathfinder.startcoordinates);
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {

            Vector3 startPosition = transform.position;
            Vector3 endPosition = gridmanager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;
            

            transform.LookAt(endPosition);
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }



        }
        FinishPath();
    }

    private void FinishPath()
    {
        gameObject.SetActive(false);
        enemy.StealGold();
        gameObject.SetActive(true);
    }
}
