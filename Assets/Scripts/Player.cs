using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameInput _gameInput;
    private CharacterController _controller;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpStrength = 15.0f;
    [SerializeField] private float _gravity;
    [SerializeField] private bool _groundPlayer;

    [SerializeField] private float _coinsCollected;
   
    private Vector3 _standingUp;
    
    private float _yVelocity;
    public float XVelocity { get; private set; }

    private bool _flipAxis;
    public bool Is_Running { get; private set;}
    public bool Is_Jumping { get; private set; }
    public bool Is_Hanging { get; set; }
    public bool Is_Climbing { get; set; }

    private void Awake()
    {
         _gameInput = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
        if (_gameInput == null) Debug.LogError("Missing Game Input");
        if (_controller == null) Debug.LogError("Missing Character Controller");
    }


    private void Update()
    {
        
        if(!Is_Hanging)
        Movement();
    }


    private void Movement() 
    {

        _groundPlayer = _controller.isGrounded;
        Is_Jumping = _gameInput.IsJumping();

        if (_groundPlayer == true && !Is_Climbing)
        {
            _controller.Move(Vector3.zero);
            _yVelocity = -_gravity;
        }

        if (_groundPlayer == true && _gameInput.IsJumping())
        {
            _yVelocity += _jumpStrength;
        }

        if (_groundPlayer == false && !Is_Climbing)
            _yVelocity -= _gravity * Time.deltaTime;

        var _yMaxVelocity = Mathf.Clamp(_yVelocity, -20, 100f);

        Vector3 _direction = _gameInput.GetMovementVectorNoramlized();
        Vector3 _xVelocity = _direction * _speed;
        
        if(_direction.x == -1 && !_flipAxis) 
        {
            _flipAxis = true;
            transform.Rotate(Vector3.up, 180);
        }
        else if (_direction.x == 1 && _flipAxis) 
        {
            _flipAxis = false;
            transform.Rotate(Vector3.up, 180);
        }


        if (Is_Climbing == true)
        {
            _yVelocity = 0;
            Vector3 ladderDirection = _gameInput.GetLadderValueNormalized();
            _yVelocity += ladderDirection.y * 2f;

        }


        Vector3 _movement = new Vector3(0, _yMaxVelocity, _xVelocity.x);
        XVelocity = _direction.x;
        Is_Running = _direction != Vector3.zero;
        _controller.Move(_movement * Time.deltaTime);

    }

    public void UpdateCoinsCollected()
    {
        _coinsCollected++;
    }

    public void GrabLedge(Vector3 snapPosition)
    {
        transform.position = snapPosition;
        Is_Hanging = true;
        XVelocity = 0;
        Is_Jumping = false;
        _controller.enabled = false;
    }

    public void SetStandingUpPosition(Vector3 snapPosition) => _standingUp = snapPosition;

    public void StandingUp() { 
    
        transform.position = _standingUp;
        Is_Hanging = false;
        _controller.enabled = true;
    }
}
