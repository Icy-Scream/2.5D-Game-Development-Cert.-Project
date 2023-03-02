using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    private Player _player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("LedgeChecker")) 
        {
            Debug.Log("Check1");
            _player = FindObjectOfType<Player>();
            if(_player != null) 
            {
                Debug.Log("LEDGE");
                _player.FreezePlayer(0f);
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
                _player.FreezePlayer(9.8f);
                _player.Is_Hanging = false;
            }
        }
    }
}
