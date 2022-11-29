using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightSensor : MonoBehaviour
{
    [SerializeField] private float sightRadius;
    public float distanceToPlayer;
    // Update is called once per frame
    void Update()
    {
        
    }

    public bool InSight(string tag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRadius);
        
        foreach(Collider col in hitColliders)
        {
            if(col.gameObject.CompareTag(tag))
            {
                distanceToPlayer = Vector3.Distance(this.transform.position, col.gameObject.transform.position);
                return true;
            }
        }
        return false;
    }
}
