using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildHitBox : MonoBehaviour
{
    [SerializeField] Transform hitBoxParentTransform;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Child hit box hit");
        hitBoxParentTransform.GetComponent<Drones>().CollisionDetection(collision);
    }
}
