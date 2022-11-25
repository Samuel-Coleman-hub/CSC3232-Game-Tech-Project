using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildHitBox : MonoBehaviour
{
    public enum DroneHitBoxType
    {
        Eye,
        LeftPropellor,
        RightPropellor
    }

    [SerializeField] DroneHitBoxType droneHitBoxType;

    [SerializeField] Transform hitBoxParentTransform;

    private void OnCollisionEnter(Collision collision)
    {
        hitBoxParentTransform.GetComponent<DroneStateManager>().OnCollisionChildEnter(droneHitBoxType, this.gameObject);
    }
}
