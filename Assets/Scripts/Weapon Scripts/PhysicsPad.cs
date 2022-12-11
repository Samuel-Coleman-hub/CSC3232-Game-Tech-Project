using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPad : MonoBehaviour
{
    public enum PadType
    {
        Bounce,
        Speed,
        Slow
    }

    public PadType type;
    public PlayerMovement playerMovement;

    public float bounceForce;
    public float speedIncrease;
    public float massIncrease;

    private Rigidbody rb;

    private float initalPlayerSpeed;
    public float initalMass = 1;

    private void Start()
    {
        if(playerMovement != null)
        {
            initalPlayerSpeed = playerMovement.runSpeed;
        }
        else
        {
            initalPlayerSpeed = 7;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody>();
            switch (type)
            {
                case PadType.Bounce:
                    //playerMovement.jumpForce = playerMovement.jumpForce * 4;
                    rb.AddForce(collision.transform.up * bounceForce, ForceMode.Impulse);
                    break;
                case PadType.Speed:
                    playerMovement.runSpeed *= speedIncrease;
                    break;
                case PadType.Slow:
                    rb.mass = massIncrease;
                    break;
            }
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            switch (type)
            {
                case PadType.Slow:
                    rb.mass = initalMass;
                    break;
                case PadType.Speed:
                    playerMovement.runSpeed = initalPlayerSpeed;
                    break;

            }
        }
    }

}
