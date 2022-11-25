using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveStateMachine : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float runSpeed;
    public float slideSpeed;

    [HideInInspector] public float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public Transform orientation;
    private bool readyToJump = true;

    public SlidingMovement slidingMovement;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    private bool onGround;

    [Header("Slope Handline")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitSlope;

    [HideInInspector] public float horizontalInput;
    [HideInInspector] public float verticalInput;

    public Rigidbody rb;
    public float velocityForCameraShake;
    private bool cameraShaking;

    //State Variables
    public PlayerMoveBaseState currentState;
    public PlayerMoveRunState runState = new();
    public PlayerMoveJumpState jumpState = new();
    public PlayerMoveIdleState idleState = new();
    public PlayerMoveGroundedState groundedState = new();
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentState = runState;
        currentState.EnterState(this);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        PlayerInput();

        //onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        //if (onGround)
        //{
        //    currentState = groundedState;
        //    currentState.EnterState(this);
        //}
        //else
        //{
        //    currentState = jumpState;
        //    currentState.EnterState(this);
        //}

        currentState.UpdateState(this);
    }
    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        
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

    public void SwitchState(PlayerMoveBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
