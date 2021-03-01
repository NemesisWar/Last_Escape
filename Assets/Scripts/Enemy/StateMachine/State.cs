using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public abstract class State : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent NavMeshAgent;
    [SerializeField] protected Animator Animator;
    [SerializeField] protected Enemy Enemy;

    [SerializeField] private List<Transitions> _transitions;

    private void OnEnable()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        Enemy = GetComponent<Enemy>();
    }

    protected Player Target { get; set; }

    public void Enter(Player target)
    {
        if(enabled == false)
        {
            Target = target;
            enabled = true;
            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }
        return null;
    }
}
