using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOnPlayerState : State
{
    private void Update()
    {
        NavMeshAgent.SetDestination(Enemy.FoundTarget.LastVisiblePlayerPosition);
        Animator.SetBool("Walk", true);
        Animator.SetBool("Attack", false);
    }
}
