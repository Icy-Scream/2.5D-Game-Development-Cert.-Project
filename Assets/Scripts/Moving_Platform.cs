using System.Collections;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private GameObject _elevatorConsole;
    [SerializeField] private float _speed;
    private bool _returnPlatform;
    private bool _startCoroutine = false;


    private void Start()
    {
        _elevatorConsole.GetComponent<Trigger_Elevator>().OnCallElevator += Moving_Platform_OnCallElevator;
    }

    private void Moving_Platform_OnCallElevator(object sender, System.EventArgs e)
    {
        if (!_startCoroutine)
            StartCoroutine(MovingPlatformRoutine());
    }

    private IEnumerator MovingPlatformRoutine() 
    {
        _startCoroutine = true;

        while(true)
        {

            if (!_returnPlatform)
            {
               transform.position = Vector3.MoveTowards(transform.position, _endPoint.position, _speed * Time.deltaTime);
                if (transform.position == _endPoint.position)
                {
                    transform.position = _endPoint.position;
                    yield return new WaitForSeconds(5f);
                    _returnPlatform = true;
                }
            }
            else if (_returnPlatform)
            {
                transform.position = Vector3.MoveTowards(transform.position, _startingPoint.position, _speed * Time.deltaTime);
                if (transform.position == _startingPoint.position)
                {
                    transform.position = _startingPoint.position;
                    yield return new WaitForSeconds(5f);
                    _returnPlatform = false;
                }
            }
            yield return new WaitForFixedUpdate();
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

}
