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
        _navMeshAgent.SetDestination(_enemy.Target.transform.position);
        if (_attackDelayTime < _time && _enemy.Target.TryGetComponent(out Player player))
        {
            player.TakeDamage(_enemy.Damage);
            _time = 0;
        }
        _animator.SetBool("Walk", false);
        _animator.SetBool("Attack", true);
    }

}
