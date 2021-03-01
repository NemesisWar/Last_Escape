using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    public event UnityAction <Enemy> Dying;
    public Player Target => _target;
    public FoundTarget FoundTarget => _foundTarget;
    public float Damage => _damage;

    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Player _target;
    [SerializeField] private FoundTarget _foundTarget;
    [SerializeField] private float _damage;
    private Animator _animator;
    private EnemyStateMachine _enemyStateMachine;

    private void OnValidate()
    {
        if (_health >= _maxHealth)
            _health = _maxHealth;
    }
    private void Start()
    {
        _foundTarget = GetComponentInChildren<FoundTarget>();
        _animator = GetComponent<Animator>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
    }

    public void Init(Player player)
    {
        _target = player;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Dying?.Invoke(this);
        _enemyStateMachine.StopMachine();
        _animator.SetBool("Die", true);
        Destroy(this.gameObject, 10f);
    }
}
