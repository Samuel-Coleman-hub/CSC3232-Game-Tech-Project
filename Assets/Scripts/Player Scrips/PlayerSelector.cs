using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask whatCanSelect;
    private Transform cameraPosition;
    private RaycastHit previousHit;


    private void Start()
    {
        cameraPosition = Camera.main.transform;
    }

    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cameraPosition.position, cameraPosition.forward, out hit, maxDistance, whatCanSelect))
        {
            hit.transform.gameObject.SendMessage("OnChildLookAt");
            previousHit = hit;

            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.gameObject.SendMessage("BuyItem");
            }
        }
        else if(previousHit.transform != null)
        {
            previousHit.transform.gameObject.SendMessage("StoppedLookingAt");
        }
    }
}
