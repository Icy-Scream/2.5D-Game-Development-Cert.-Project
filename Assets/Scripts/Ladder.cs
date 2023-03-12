using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            
            other.gameObject.TryGetComponent(out Player player);
            if (player.gameObject.GetComponent<GameInput>().GrabInteraction())
            {
                player.Is_Climbing = true;
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y,this.transform.position.z + 0.5f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.TryGetComponent(out Player player);
            player.Is_Climbing = false;
        }
    }
}
