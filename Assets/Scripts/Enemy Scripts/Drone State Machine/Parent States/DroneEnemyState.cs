using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneEnemyState : DroneBaseState
{
    private DroneStateManager _sm;
    private bool leftPropellorHit;
    private bool rightPropellorHit;
    public DroneEnemyState(string name ,DroneStateManager stateMachine) : base(name, stateMachine)
    {
        _sm = (DroneStateManager)stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("we in an enemystate inherited class");
        base.EnterState();
        
        stateMachine.patrolLight.color = stateMachine.lightAttackColour;
        stateMachine.patrolLightMeshRenderer.material = stateMachine.lightAttackMaterial;
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void OnCollisionChildEnter(ChildHitBox.DroneHitBoxType hitBoxType, GameObject objHit)
    {
        base.OnCollisionChildEnter(hitBoxType, objHit);

        switch (hitBoxType)
        {
            case ChildHitBox.DroneHitBoxType.Eye:
                stateMachine.SwitchState(_sm.enemyIdleState);
                stateMachine.eyeObj.SetActive(false);
                break;
            case ChildHitBox.DroneHitBoxType.LeftPropellor:
                leftPropellorHit = true;
                stateMachine.leftPropellorObj.SetActive(false);
                break;
            case ChildHitBox.DroneHitBoxType.RightPropellor:
                Debug.Log("right propellor hit");
                rightPropellorHit = true;
                stateMachine.rightPropellorObj.SetActive(false);
                break;
        }

        Debug.Log("Just before check bools");
        if (leftPropellorHit && rightPropellorHit)
        {
            Debug.Log("Both propellors broken");
            stateMachine.SwitchState(_sm.offIdleState);
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
