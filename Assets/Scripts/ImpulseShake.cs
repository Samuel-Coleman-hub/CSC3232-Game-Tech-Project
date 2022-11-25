using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseShake : MonoBehaviour
{
    private CinemachineImpulseSource impulse;
    void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
    }

    public void Shake()
    {
        impulse.GenerateImpulse();
    }

}
