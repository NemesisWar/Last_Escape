using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnPoint> _enemySpawnPoints;
    [SerializeField] private Player _player;
    private Coroutine _createCorounine;

    private void Start()
    {
        _enemySpawnPoints.AddRange(GetComponentsInChildren<EnemySpawnPoint>());
        OnEnable();
    }

    private void OnEnable()
    {
        if (_createCorounine == null)
        {
            _createCorounine = StartCoroutine(FindRandomPiontToCreateEmeny());
        }
    }

    private void TryGetAllCreaterEnemys()
    {
        foreach (var point in _enemySpawnPoints)
        {
            if (!point.EnemyIsAlive)
                return;
        }
        StopCoroutine(_createCorounine);
        _createCorounine = null;
        gameObject.SetActive(false);
    }

    private IEnumerator FindRandomPiontToCreateEmeny()
    {
        var waitForOneSeconds = new WaitForSeconds(2f);
        foreach (var point in _enemySpawnPoints)
        {
            point.CheckCreateEnemyInPoint(_player);
            TryGetAllCreaterEnemys();
            yield return waitForOneSeconds;
        }
    }
}
