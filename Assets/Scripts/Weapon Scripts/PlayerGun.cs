using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : Shooter
{
    [Header("Gun Settings")]
    public int mouseButtonInt;
    public Material laserGunMaterial;
    public MeshRenderer gunBody;


    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButton(mouseButtonInt))
        {
            StartShooting();
        }
    }


    public void SwitchToLaserGun()
    {
        shotCooldown = 0.1f;
        gunBody.material = laserGunMaterial;
    }

}
