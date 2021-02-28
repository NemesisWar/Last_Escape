using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transitions : MonoBehaviour
{
    [SerializeField] private State _targetState;
    protected Player Target{get; private set;}
    protected Enemy _enemy { get; private set; }

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(Player target)
    {
        Target = target;
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
