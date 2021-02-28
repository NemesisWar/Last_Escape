using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    [SerializeField] private Player _target;
    [SerializeField] private State _currentState;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    public State Current => _currentState;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _target = GetComponent<Enemy>().Target;
        Reset(_firstState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;
        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }
    private void Reset(State startState)
    {
        _currentState = startState;
        if (_currentState != null)
            _currentState.Enter(_target);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }

    public void StopMachine()
    {
        _currentState.Exit();
        _navMeshAgent.enabled = false;
    }
}
