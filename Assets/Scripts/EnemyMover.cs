using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    Enemy enemy;

    
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
        
    }

    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
            {
                path.Add(waypoint);
            }
            
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint points in path)
        {

            Vector3 startPosition = transform.position;
            Vector3 endPosition = points.transform.position;
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
