using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Movement")]
    private float movementSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float slideSpeed;

    [SerializeField] private float desiredMoveSpeed;
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

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    public float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;

    [Header("Slope Handline")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitSlope;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Rigidbody rb;
    public bool sliding = false;


    //Bools for state switch
    private bool onGround;
    private bool isCrouchPressed;


    //State Variables
    PlayerBaseState currentState;
    PlayerStateFactory states;

    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }

    //GameObject Get & Set
    public bool OnGround { get { return onGround; } }
    public Transform PlayerTransform { get { return rb.transform; } }
    public Rigidbody PlayerRigidbody { get { return rb; } }

    //Crouch Get & Set
    public bool IsCrouchPressed { get { return isCrouchPressed; } }
    public float CrouchYScale { get { return crouchYScale; } }
    public float StartYScale { get { return startYScale; } }

    //Movement Get & Set
    public float HorizontalInput { get { return horizontalInput; } }
    public float VerticalInput { get { return verticalInput; } }
    public float MovementSpeed { get { return movementSpeed; } }   
    public Vector3 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
    public Transform Orientation { get { return orientation; } }

    //Sliding Get & Set
    public bool Sliding { get { return sliding; } }
    public SlidingMovement SlidingMovement { get { return slidingMovement; } }
    public bool isOnSlope { get { return OnSlope(); } }
    public bool ExitSlope { get { return exitSlope; } }

    //Air Get & Set
    public float AirMultiplier { get { return airMultiplier; } }




    private void Awake()
    {
        states = new PlayerStateFactory(this);
        currentState = states.Walk();
        currentState.EnterState();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        currentState.UpdateState();

        PlayerInput();
        //SpeedControl();

        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
    }

    private void SpeedControl()
    {
        if (OnSlope() && !exitSlope)
        {
            if (rb.velocity.magnitude > movementSpeed)
            {
                rb.velocity = rb.velocity.normalized * movementSpeed;
            }
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > movementSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * movementSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && onGround)
        {
            //readyToJump = false;
            //Jump();
            //Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private bool OnSlope()
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
