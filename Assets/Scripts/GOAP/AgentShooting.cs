using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentShooting : Shooter
{
    private bool currentlyShooting;

    public override void Update()
    {
        base.Update();
        if (currentlyShooting)
        {
            base.StartShooting();
        }
    }

    public void ShootingActive()
    {
        currentlyShooting = true;
    }

    public void ShootingDeactivate()
    {
        currentlyShooting = false;
    }


}
