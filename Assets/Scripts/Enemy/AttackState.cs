using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private float _attackDelayTime;
    [SerializeField] private float _time;

    private void Update()
    {
        _time += Time.deltaTime;
        NavMeshAgent.SetDestination(Enemy.Target.transform.position);
        if (_attackDelayTime < _time && Enemy.Target.TryGetComponent(out Player player))
        {
            player.TakeDamage(Enemy.Damage);
            _time = 0;
        }
        Animator.SetBool("Walk", false);
        Animator.SetBool("Attack", true);
    }
}
