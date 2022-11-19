using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementStateManager : MonoBehaviour
{
    public Transform orientation;

    //Player Components
    [Header("GameObject Properties")]
    public Rigidbody rb;

    //Movement Variables
    public float movementSpeed;
    public Vector3 moveDirection;

    //Air Variables
    public float airMultiplier;

    //Jump Variables
    public float jumpForce;
    public float jumpCooldown;
    private bool readyToJump = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool onGround;

    [Header("Slope Handline")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    public bool exitSlope;

    //Sliding Variables
    public SlidingMovement slidingMovement;
    public bool sliding = false;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public float horizontalInput;
    public float verticalInput;

    //Different States
    public PlayerMovementBaseState currentState;
    public PlayerMovementRunState RunState = new PlayerMovementRunState();
    public PlayerMovementCrouchState CrouchState = new PlayerMovementCrouchState();
    public PlayerMovementAirState AirState = new PlayerMovementAirState();
    public PlayerMovementSlidingState SlidingState = new PlayerMovementSlidingState();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        currentState = RunState;
        currentState.EnterState(this);

    }

    private void Update()
    {
        currentState.UpdateState(this);

        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        PlayerInput();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(PlayerMovementBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && onGround)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void Jump()
    {
        exitSlope = true;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitSlope = false;
    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 dir)
    {
        return Vector3.ProjectOnPlane(dir, slopeHit.normal).normalized;
    }

}
