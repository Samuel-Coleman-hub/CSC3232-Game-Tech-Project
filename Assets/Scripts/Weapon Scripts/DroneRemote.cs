using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneRemote : MonoBehaviour
{
    public Transform beamPoint;
    public Transform cameraPosition;
    public float maxDistance;
    public LayerMask whatCanFlyTo;
    public int mouseButtonInt;

    private LineRenderer lineRenderer;
    [HideInInspector]public Vector3 beamDes;
    private bool currentlyBeaming;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, beamPoint.position);
        cameraPosition = Camera.main.transform;
    }

    private void Update()
    {
        if (Input.GetMouseButton(mouseButtonInt))
        {
            Beaming();
        }
        else if (Input.GetMouseButtonUp(mouseButtonInt))
        {
            StopBeam();
        }
        
    }

    private void LateUpdate()
    {
        DrawBeam();
    }

    private void Beaming()
    {
        currentlyBeaming = true;
        lineRenderer.positionCount = 2;
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, maxDistance))
        {
            beamDes = hit.point;

            if (hit.transform.gameObject.tag == "Drone")
            {
                hit.transform.SendMessage("HitByDroneRemote");
            }

        }
        else
        {
            beamDes = cameraPosition.position + cameraPosition.forward * maxDistance;
        }
    }

    private void StopBeam()
    {
        currentlyBeaming=false;
        lineRenderer.positionCount = 0;
    }

    private void DrawBeam()
    {
        if (!currentlyBeaming)
        {
            return;
        }
        lineRenderer.SetPosition(0, beamPoint.position);
        lineRenderer.SetPosition(1, beamDes);
    }
}
