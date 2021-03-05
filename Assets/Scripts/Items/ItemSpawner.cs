using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField, HideInInspector] private List<SpawnPointItem> _spawnWeaponPoints;
    [SerializeField, HideInInspector] private List<SpawnPointItem> _spawnAmmoPoints;
    [SerializeField, HideInInspector] private List<SpawnPointItem> _spawnFirstAidKitPoints;
    [SerializeField, HideInInspector] private List<SpawnPointItem> _spawnKeysPoints;
    [SerializeField] private List<ItemData> _weaponItem;
    [SerializeField] private List<ItemData> _ammoItem;
    [SerializeField] private List<ItemData> _firstAidKitItem;
    [SerializeField] private List<ItemData> _keyItem;

    private int _numberPointToSpawn;

    private void Awake()
    {
        _spawnWeaponPoints.AddRange(GetComponentsInChildren<SpawnPointItem>().Where(spawnPoint => spawnPoint.Type.ToString().Contains("Weapon")));
        _spawnAmmoPoints.AddRange(GetComponentsInChildren<SpawnPointItem>().Where(spawnPoint => spawnPoint.Type.ToString().Contains("Ammo")));
        _spawnFirstAidKitPoints.AddRange(GetComponentsInChildren<SpawnPointItem>().Where(spawnPoint => spawnPoint.Type.ToString().Contains("FirstAidKit")));
        _spawnKeysPoints.AddRange(GetComponentsInChildren<SpawnPointItem>().Where(spawnPoint => spawnPoint.Type.ToString().Contains("Key")));
    }

    private void Start()
    {
        InstantiateObjectsInRandomPoints(_spawnWeaponPoints, _weaponItem);
        InstantiateObjectsInRandomPoints(_spawnAmmoPoints, _ammoItem);
        InstantiateObjectsInRandomPoints(_spawnFirstAidKitPoints, _firstAidKitItem);
        InstantiateObjectsInRandomPoints(_spawnKeysPoints, _keyItem);
    }

    private void InstantiateObjectsInRandomPoints(List<SpawnPointItem> points, List<ItemData> DictionaryItem)
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

    private bool ItemNotSpawnedInThisPoint(SpawnPointItem spawnpoint)
    {
        return spawnpoint.ItemSpawned;
    }
}
