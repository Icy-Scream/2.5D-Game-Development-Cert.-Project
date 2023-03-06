using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Moving_Platform : MonoBehaviour
{
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _speed;
    private Vector3 _direction;
    private bool _returnPlatform;
    private float _distance;
    private float _startingDistance;
    private bool _startCoroutine = false; 

    void Start()
    {
        transform.position = _startingPoint.position;
        _direction = _endPoint.position - transform.position;
        _startingDistance = _direction.magnitude;
        
    }


    private void Update()
    {
        if(!_startCoroutine)
            StartCoroutine(MovingPlatformRoutine());
    }
    private IEnumerator MovingPlatformRoutine() 
    {
        _startCoroutine = true;

        while(true)
        {

            _direction = _endPoint.position - transform.position;
            _distance = _direction.magnitude;
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
       
}
