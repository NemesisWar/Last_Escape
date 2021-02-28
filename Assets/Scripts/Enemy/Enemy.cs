using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private Player _target;
    [SerializeField] private FoundTarget _foundTarget;
    [SerializeField] private float _damage;
    private CapsuleCollider _capsuleCollider;
    private Animator _animator;
    public event UnityAction <Enemy> Dying;
    private EnemyStateMachine _enemyStateMachine;

    public Player Target => _target;
    public FoundTarget FoundTarget => _foundTarget;
    public float Damage => _damage;

    private void OnValidate()
    {
        if (_health >= _maxHealth)
            _health = _maxHealth;
    }
    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
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
