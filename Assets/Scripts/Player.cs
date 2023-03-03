
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameInput _gameInput;
    private CharacterController _controller;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpStrength = 15.0f;
    [SerializeField] private float _gravity;
    [SerializeField] private bool _groundPlayer;
    private float _yVelocity;
    private bool _flipAxis;
    public bool Is_Running { get; private set;}
    public bool Is_Jumping { get; private set; }
    public bool Is_Hanging { get; set; }

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

        if (_groundPlayer == true)
        {
            _controller.Move(Vector3.zero);
            _yVelocity = -_gravity;
        }

        if (_groundPlayer == true && _gameInput.IsJumping())
        {
            _yVelocity += _jumpStrength;
        }

        if (_groundPlayer == false)
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

        Vector3 _movement = new Vector3(_xVelocity.x, _yMaxVelocity, 0);
        Is_Running = _direction != Vector3.zero;
        _controller.Move(_movement * Time.deltaTime);

    }

    public void FreezePlayer()
    {
        _controller.enabled = false;
    }

}
