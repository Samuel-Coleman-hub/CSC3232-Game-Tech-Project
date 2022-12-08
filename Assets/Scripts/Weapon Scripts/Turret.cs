using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Shooter
{
    [Header("Turret Settings")]
    [SerializeField] private float SightRadius;
    [SerializeField] private LayerMask whatIsEnemy;

    private GameObject targetEnemy;


    public override void Update()
    {
        base.Update();
        if (targetEnemy == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, SightRadius, whatIsEnemy);
            targetEnemy = FindClosestEnemy(hitColliders);
        }
        else
        {
            base.StartShooting();
            Vector3 lookAtPosition = new Vector3(targetEnemy.transform.position.x, transform.position.y, targetEnemy.transform.position.z);
            transform.LookAt(lookAtPosition);
            if(Vector3.Distance(this.transform.position, targetEnemy.transform.position) > SightRadius)
            {
                targetEnemy = null;
            }
        }
    }

    private GameObject FindClosestEnemy(Collider[] hitColliders)
    {
        GameObject closestEnemy = null;
        float closestDistance = SightRadius;
        foreach(Collider c in hitColliders)
        {
            float distance = Vector3.Distance(this.transform.position, c.transform.position);
            
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = c.gameObject;
            }
        }
        return closestEnemy;
    }

    
}
