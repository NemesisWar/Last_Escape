using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOnPlayerState : State
{
    private void Update()
    {
        _navMeshAgent.SetDestination(_enemy.FoundTarget.LastVisiblePlayerPosition);
        _animator.SetBool("Walk", true);
        _animator.SetBool("Attack", false);
    }
}
