using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transitions : MonoBehaviour
{
    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set;}
    protected Player Target{get; private set;}
    protected Enemy Enemy { get; private set; }

    [SerializeField] private State _targetState;

    public void Init(Player target)
    {
        Target = target;
        Enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
