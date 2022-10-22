using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingMovement : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody playerRb;
    private PlayerMovement playerMovement;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<PlayerMovement>();    

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0) && playerMovement.movementState != PlayerMovement.MovementState.air)
        {
            StartSliding();
        }

        if(Input.GetKeyUp(slideKey) && playerMovement.sliding)
        {
            StopSliding();
        }
        
    }

    private void FixedUpdate()
    {
        if (playerMovement.sliding)
        {
            HandleMovement();
        }
    }

    private void StartSliding()
    {
        playerMovement.sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        playerRb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void HandleMovement()
    {
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(!playerMovement.OnSlope() || playerRb.velocity.y > -0.1f)
        {
            playerRb.AddForce(inputDir.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }
        else
        {
            playerRb.AddForce(playerMovement.GetSlopeMoveDirection(inputDir) * slideForce, ForceMode.Force);
        }

        

        if(slideTimer <= 0)
        {
            StopSliding();
        }
    }

    private void StopSliding()
    {
        playerMovement.sliding = false;
        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
    }
}
