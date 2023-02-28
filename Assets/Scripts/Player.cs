
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameInput _gameInput;
    private CharacterController _controller;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _jumpStrength = 15.0f;
    [SerializeField] private float _gravity = 1.0f;
    [SerializeField] private bool _groundPlayer;
    private float _yVelocity;
    public bool Is_Running { get; private set;}


    private void Awake()
    {
         _gameInput = GetComponent<GameInput>();
        _controller = GetComponent<CharacterController>();
        if (_gameInput == null) Debug.LogError("Missing Game Input");
        if (_controller == null) Debug.LogError("Missing Character Controller");
    }

    private void Update()
    {
        Movement();
    }


    private void Movement() 
    {

        _groundPlayer = _controller.isGrounded;


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

        Vector3 _movement = new Vector3(_xVelocity.x, _yMaxVelocity, 0);
        Is_Running = _direction != Vector3.zero;
        _controller.Move(_movement * Time.deltaTime);

    }

}
