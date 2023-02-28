using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [SerializeField] Player _player;
    private const string IS_RUNNING = "Running";
    private Animator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        _playerAnimator.SetBool(IS_RUNNING, _player.Is_Running);
    }
}
