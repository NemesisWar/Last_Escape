using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    [SerializeField] private float minRandomValue;
    [SerializeField] private float maxRandomValue;
    [SerializeField] private Vector3 _targetMove;
    [SerializeField] private float _offSet;
    [SerializeField] private float _time;
    [SerializeField] private float _maxTimeToStayInPosition;
    [SerializeField] private Vector3 _positionTimeCheck;

    private void Start()
    {
        _positionTimeCheck = transform.position;
        SetNewVectorToMove();
        _time = 0;
        _navMeshAgent.speed = 6;
    }
    private void Update()
    {
        _time += Time.deltaTime;
        _navMeshAgent.SetDestination(_targetMove);
        if(Vector3.Distance(transform.position, _targetMove)<=_offSet)
        {
            SetNewVectorToMove();
        }
        if (_time >= _maxTimeToStayInPosition)
        {
            _time = 0;
            if (Vector3.Distance(transform.position, _positionTimeCheck) < 0.2f)
            {
                SetNewVectorToMove();
            }
            _positionTimeCheck = transform.position;
        }
        transform.LookAt(_enemy.transform.position);
    }

    private void SetNewVectorToMove()
    {
        _targetMove = transform.position;
        _targetMove.x += Random.Range(minRandomValue, maxRandomValue);
        _targetMove.y = 100f;
        _targetMove.z += Random.Range(minRandomValue, maxRandomValue);
        if(Physics.Raycast(_targetMove, Vector3.down, out RaycastHit hit))
        {
            if (hit.point.y < 52f)
            {
                SetNewVectorToMove();
            }
            else
            {
                _targetMove = hit.point;
            }
        }
        _animator.SetBool("Walk", true);
        _animator.SetBool("Attack", false);
    }
}
