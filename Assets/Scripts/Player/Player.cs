using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [HideInInspector] public float Health => _health;
    [HideInInspector] public float Stamina => _stamina;
    public event UnityAction Dying;

    [SerializeField] private float _health;
    [SerializeField] private float _stamina;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _maxStamina;
    [SerializeField] private UnityEvent _playerHealthHasBeenChanged;
    [SerializeField] private UnityEvent _playerStaminaHasBeenChanged;

    public event UnityAction ChangeHealth
    {
        add => _playerHealthHasBeenChanged.AddListener(value);
        remove => _playerHealthHasBeenChanged.RemoveListener(value);
    }

    public event UnityAction ChangeStamina
    {
        add => _playerStaminaHasBeenChanged.AddListener(value);
        remove => _playerStaminaHasBeenChanged.RemoveListener(value);
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _playerHealthHasBeenChanged?.Invoke();
        if (_health <= 0)
        {
            Die();
        }
    }

    public void AddHealth(float addHealth)
    {
        _health += addHealth;
        _playerHealthHasBeenChanged?.Invoke();
    }

    public void SpendingStamina(float spendStamina)
    {
        _stamina -= spendStamina;
        _playerStaminaHasBeenChanged?.Invoke();
    }

    public void AddStamina(float addStamina)
    {
        if (_stamina > _maxStamina)
            return;
        _stamina += addStamina;
        _playerStaminaHasBeenChanged?.Invoke();
    }

    public void Die()
    {
        Dying?.Invoke();
        MonoBehaviour[] scripst = GetComponents<MonoBehaviour>();
        foreach (var script in scripst)
        {
            if(script.GetType().Name!="PlayerAnimation")
            {
                script.enabled = false;
            }
        }
    }
    private void OnValidate()
    {
        if (_health >= _maxHealth)
            _health = _maxHealth;
        if (_stamina >= _maxStamina)
            _stamina = _maxStamina;
    }
}
