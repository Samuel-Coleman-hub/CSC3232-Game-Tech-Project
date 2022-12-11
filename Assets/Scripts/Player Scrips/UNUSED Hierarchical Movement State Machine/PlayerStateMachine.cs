using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //State Machine References
    PlayerBaseState currentState;
    PlayerStateFactory stateFactory;

    //Component/GameObject Variables
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float playerHeight;

    //Movement Variables
    private bool isMovementPressed;
    private bool isJumpPressed;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    [SerializeField] Transform orientation;
    [SerializeField] float movementSpeed;

    //Jump variables
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask whatIsGround;

    private bool onGround;

    //Keycodes
    [SerializeField] private KeyCode jumpKey;

    //Get and Set Variable methods
    public PlayerBaseState CurrentState { get { return currentState; } set { currentState = value; } }
    public Rigidbody Rb { get { return rb; } }

    public bool OnGround { get { return onGround; } }
    public bool IsMovementPressed { get { return isMovementPressed; } }
    public float HorizontalInput { get { return horizontalInput; } }
    public float VerticalInput { get { return verticalInput; } }
    public Vector3 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
    public Transform Orientation { get { return orientation;} }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }


    public bool IsJumpPressed { get { return isJumpPressed; } set { isJumpPressed = value; } }
    public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }



    // Start is called before the first frame update
    private void Awake()
    {
        stateFactory = new PlayerStateFactory(this);
        currentState = stateFactory.Grounded();
        currentState.EnterState();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        currentState.UpdateStates();
        onGround = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        PlayerInput();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(horizontalInput != 0 || verticalInput != 0)
        {
            isMovementPressed = true;
        }
        else
        {
            isMovementPressed = false;
        }

        if (Input.GetKeyDown(jumpKey) && onGround)
        {
            isJumpPressed = true;
        }
        else
        {
            isJumpPressed=false;
        }
    }
}
