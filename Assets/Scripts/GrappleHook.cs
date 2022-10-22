using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [Header("Grapple Settings")]
    public LayerMask whatCanGrappleOn;
    public Transform grapplePoint;
    public Transform cameraPosition;
    public Transform player;
    public float maxDistance = 100f;

    private SpringJoint joint;
    private LineRenderer lineRenderer;
    private Vector3 grappleDes;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrappling();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrappling();
        }
    }

    private void LateUpdate()
    {
        DrawGrappleLine();
    }

    private void StartGrappling()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, maxDistance, whatCanGrappleOn))
        {
            grappleDes = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grappleDes;

            float distanceFromPoint = Vector3.Distance(player.position, grappleDes);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lineRenderer.positionCount = 2;
        }
    }

    private void StopGrappling()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }

    private void DrawGrappleLine()
    {
        if (!joint)
        {
            return;
        }

        lineRenderer.SetPosition(0, grapplePoint.position);
        lineRenderer.SetPosition(1, grappleDes);
    }


}
