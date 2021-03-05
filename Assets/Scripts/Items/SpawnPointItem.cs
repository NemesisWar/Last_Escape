using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointItem : MonoBehaviour
{
    public ItemCharacter Type;
    public bool ItemSpawned => _itemSpawned;

    [SerializeField] private bool _itemSpawned;

    public enum ItemCharacter
    {
        Weapon = 0,
        Ammo = 1,
        FirstAidKit = 2,
        Key = 3
    }

    public void InstantiatedItem(ItemData item)
    {
        var CratenItem = Instantiate(item.Prefab, transform.position, transform.rotation);
        _itemSpawned = true;
    }
}
