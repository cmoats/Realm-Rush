using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] [Range(0,50)] int PoolSize =5;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {

            EnableObject();
            yield return new WaitForSeconds(3f);
            Debug.Log("isruun0");
        }
    }
    void EnableObject()
    {
        foreach(GameObject enemy in pool)
        {
            bool active = enemy.activeInHierarchy;
            if (!active)
            {
                enemy.SetActive(true);
                return;
            }

        }
    }
    void PopulatePool()
    {
        pool =  new GameObject[PoolSize];
        
        for(int i=0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(Enemy, transform);
            pool[i].SetActive(false);
        }

        

    }
}
