﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private bool _enemyIsAlive;
    [SerializeField] private UnityEngine.GameObject _enemyPrefab;
    public bool EnemyIsAlive=>_enemyIsAlive;

    public event UnityAction CreatedEnemy;

    private void CrateEnemy(Player player)
    {
        Enemy enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.Init(player);
        enemy.Dying += OnEnemyDying;
        _enemyIsAlive = true;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        _enemyIsAlive = false;
        enemy.Dying -= OnEnemyDying;
    }

    public void CheckCreateEnemyInPoint(Player player)
    {
        if (_enemyIsAlive)
        {
            return;
        }
        else
        {
            CrateEnemy(player);
        }
    }
}