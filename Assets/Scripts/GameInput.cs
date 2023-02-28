using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameInput : MonoBehaviour
{
    private GameMaps _gameInput;

    private void Awake()
    {
        _gameInput = new GameMaps();
    }

    private void OnEnable()
    {
        _gameInput.Player.Enable();
    }


    public Vector3 GetMovementVectorNoramlized() 
    {
       Vector3 _moveDirection = _gameInput.Player.Move.ReadValue<Vector2>();

        _moveDirection = _moveDirection.normalized;

        return _moveDirection;
    }

    public bool IsJumping()
    {
        if (_gameInput.Player.Jump.IsPressed())
            return true;
        else return false;
    }
}
