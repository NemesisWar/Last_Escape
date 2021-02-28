using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnItem> _spawnAllPoints;
    [SerializeField] private List<SpawnItem> _spawnWeaponPoints;
    [SerializeField] private List<SpawnItem> _spawnAmmoPoints;
    [SerializeField] private List<SpawnItem> _spawnFirstAidKitPoints;
    [SerializeField] private List<SpawnItem> _spawnKeysPoints;
    [SerializeField] private List<ItemData> _weaponItem;
    [SerializeField] private List<ItemData> _ammoItem;
    [SerializeField] private List<ItemData> _firstAidKitItem;
    [SerializeField] private List<ItemData> _keyItem;

    private int _numberPointToSpawn;

    private void Awake()
    {
        _spawnAllPoints.AddRange(GetComponentsInChildren<SpawnItem>());
        AddPointsOnRange();
    }

    private void Start()
    {
        InstantiateObjectsInRandomPoints(_spawnWeaponPoints, _weaponItem);
        InstantiateObjectsInRandomPoints(_spawnAmmoPoints, _ammoItem);
        InstantiateObjectsInRandomPoints(_spawnFirstAidKitPoints, _firstAidKitItem);
        InstantiateObjectsInRandomPoints(_spawnKeysPoints, _keyItem);
    }

    private void AddPointsOnRange()
    {
        foreach (var item in _spawnAllPoints)
        {
            if (((int)item.Type) == 0)
            {
                _spawnWeaponPoints.Add(item);
            }
            if (((int)item.Type) == 1)
            {
                _spawnAmmoPoints.Add(item);
            }
            if (((int)item.Type) == 2)
            {
                _spawnFirstAidKitPoints.Add(item);
            }
            if (((int)item.Type) == 3)
            {
                _spawnKeysPoints.Add(item);
            }
        }
    }

    private void InstantiateObjectsInRandomPoints(List<SpawnItem> points, List<ItemData> DictionaryItem)
    {
        foreach (var item in DictionaryItem)
        {
            bool canNotSpawn = true;
            _numberPointToSpawn = Random.Range(0, points.Count);
            while (canNotSpawn == true)
            {
                canNotSpawn = ItemNotSpawnedInThisPoint(points[_numberPointToSpawn]);
                if (canNotSpawn)
                {
                    _numberPointToSpawn = Random.Range(0, points.Count);
                }
            }
            points[_numberPointToSpawn].InstantiatedItem(item);
        }
    }

    private bool ItemNotSpawnedInThisPoint(SpawnItem spawnpoint)
    {
        return spawnpoint.ItemSpawned;
    }
}
