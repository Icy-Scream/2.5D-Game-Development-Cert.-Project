using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [SerializeField] Player _player;
    private GameInput _gameInput;
    private const string IS_JUMPING = "Jumping";
    private const string IS_HANGING = "LedgeGrab";
    private const string IS_CLIMBING = "ClimbUp";
    private const string IS_ROLLING = "Roll";
    private const string IS_LADDER = "Ladder";
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _gameInput = GetComponentInParent<GameInput>();
        if (_gameInput == null) Debug.LogError("Missing Game Input");
        _gameInput.OnInteract += _gameInput_OnInteract;
    }

    private void RollAnimation() { 
        if(_gameInput.IsRolling() && !_player.Is_Jumping && !_player.Is_Hanging)
        {
            _playerAnimator.SetTrigger(IS_ROLLING);
        }
        else
            _playerAnimator.ResetTrigger(IS_ROLLING);
    }

    private void _gameInput_OnInteract(object sender, System.EventArgs e)
    {
        if (_player.Is_Hanging) 
        { 
            _playerAnimator.SetTrigger(IS_CLIMBING);
        }
    }

    private void Update()
    {
        _playerAnimator.SetBool(IS_JUMPING, _player.Is_Jumping);
        _playerAnimator.SetFloat("Speed", Mathf.Abs(_player.XVelocity));
        _playerAnimator.SetBool(IS_HANGING, _player.Is_Hanging);
        _playerAnimator.SetBool(IS_LADDER, _player.Is_Climbing);
        RollAnimation();
    }
    
}
