using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocater : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [SerializeField] float towerRange = 15f;
    [SerializeField] ParticleSystem projectiles;
    Transform target;


    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }


    void AimWeapon()
    {
        if (target == null) { return; }

        float targetDistance = Vector3.Distance(transform.position, target.position);

        if(targetDistance > towerRange)
        {
            Attack(false);
        }
        else
        {
            Attack(true);
        }

        weapon.LookAt(target);
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach(Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void Attack(bool isActive)
    {
        var ps = projectiles.emission;
        ps.enabled = isActive;
    }
}
