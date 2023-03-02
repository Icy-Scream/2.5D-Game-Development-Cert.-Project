using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [SerializeField] Player _player;
    private const string IS_RUNNING = "Running";
    private const string IS_JUMPING = "Jumping";
    private const string IS_HANGING = "LedgeGrab";
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        _playerAnimator.SetBool(IS_RUNNING, _player.Is_Running);
        _playerAnimator.SetBool(IS_JUMPING, _player.Is_Jumping);
        _playerAnimator.SetBool(IS_HANGING, _player.Is_Hanging);
    }
}
