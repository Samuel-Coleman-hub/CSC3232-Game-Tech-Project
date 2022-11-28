using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //State Machine References
    PlayerBaseState _currentState;
    PlayerStateFactory _stateFactory;

    //Component/GameObject Variables
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _playerHeight;

    //Movement Variables
    private bool _isMovementPressed;
    private bool _isJumpPressed;
    private float _horizontalInput;
    private float _verticalInput;

    //Jump variables
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _whatIsGround;

    private bool _onGround;

    //Keycodes
    [SerializeField] private KeyCode _jumpKey;

    //Get and Set Variable methods
    public PlayerBaseState CurrentState { get { return _currentState; } set { _currentState = value; } }
    public Rigidbody Rb { get { return _rb; } }

    public bool OnGround { get { return _onGround; } }
    public bool IsMovementPressed { get { return _isMovementPressed; } }


    public bool IsJumpPressed { get { return _isJumpPressed; } set { _isJumpPressed = value; } }
    public float JumpForce { get { return _jumpForce; } set { _jumpForce = value; } }



    // Start is called before the first frame update
    private void Awake()
    {
        _stateFactory = new PlayerStateFactory(this);
        _currentState = _stateFactory.Grounded();
        _currentState.EnterState();

        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _currentState.UpdateState();
        _onGround = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _whatIsGround);
        PlayerInput();
    }

    private void PlayerInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(_horizontalInput != 0 || _verticalInput != 0)
        {
            _isMovementPressed = true;
        }
        else
        {
            _isMovementPressed = false;
        }

        if (Input.GetKeyDown(_jumpKey) && _onGround)
        {
            _isJumpPressed = true;
        }
        else
        {
            _isJumpPressed=false;
        }
    }
}
