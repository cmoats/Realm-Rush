using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int MaxHits = 4;
    [SerializeField] int DifficultyRamp = 1;

    int CurrentHitPoints;

    Enemy enemy;
    
    private void OnEnable()
    {
        CurrentHitPoints = MaxHits;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProccessHit();
    }


    private void Start()
    {
        enemy = gameObject.GetComponent<Enemy>();
    }
    private void ProccessHit()
    {
        CurrentHitPoints--;
        if (CurrentHitPoints < 1)
        {
            gameObject.SetActive(false);
            enemy.RewardGold();
            MaxHits += DifficultyRamp;
            gameObject.SetActive(true);
            
        }
    }
}
