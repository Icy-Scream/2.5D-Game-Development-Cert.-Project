using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    private Player _player;
    [SerializeField] float _snapPositionX,_snapPositionY,_snapPositionZ;
    private Vector3 _snapPosition;

    private void Start()
    {
        _snapPosition = new Vector3(_snapPositionX, _snapPositionY, _snapPositionZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("LedgeChecker")) 
        {
            Debug.Log("Check1");
            _player = FindObjectOfType<Player>();
            if(_player != null) 
            {
                Debug.Log("LEDGE");
                _player.transform.position = _snapPosition;
                _player.FreezePlayer();
                _player.Is_Hanging = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("LedgeChecker"))
        {
            Debug.Log("Check1");
            _player = FindObjectOfType<Player>();
            if (_player != null)
            {
                Debug.Log("LEDGE");
                _player.FreezePlayer();
                _player.Is_Hanging = false;
            }
        }
    }
}
