using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private WeaponHandled _weaponHandled;
    [SerializeField] private int _maxItemsInInventory;
    private Dictionary<ItemData, int> _inventory = new Dictionary<ItemData, int>(6);

    public event UnityAction<Dictionary<ItemData, int>> ChangeInventory;

    public void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("TakeItem") && other.TryGetComponent(out Item takeditem))
        {
            bool canTakeIt = CanTakeIt(takeditem);
            if (canTakeIt==true)
            {
                if (takeditem.ItemData.Stockable)
                    TakeStockableItem(takeditem);
                else
                    TakeUnstockableItem(takeditem);
            }
        }
    }

    private bool CanTakeIt(Item takedItem)
    {
        if (_inventory.Keys.Count == 0 || _inventory.Keys.Count < _maxItemsInInventory)
        {
            return true;
        }
        else if (_inventory.Count == _maxItemsInInventory && takedItem.ItemData.Stockable)
        {
            foreach (var item in _inventory)
            {
                if (item.Key.IndexItem == takedItem.ItemData.IndexItem)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void TakeStockableItem(Item takedItem)
    {
        bool taked = false;
        foreach (var item in _inventory)
        {
            if (takedItem.ItemData.IndexItem == item.Key.IndexItem)
            {
                _inventory[item.Key] = item.Value + takedItem.Count;
                ChangeInventory?.Invoke(_inventory);
                taked = true;
                Destroy(takedItem.gameObject);
                break;
            }
        }
        if(!taked)
            TakeUnstockableItem(takedItem);
    }

    private void TakeUnstockableItem(Item takedItem)
    {
        _inventory.Add(takedItem.ItemData, takedItem.Count);
        if (takedItem.TryGetComponent(out Weapon weapon))
        {
            _weaponHandled.AddAmmoInWeapon(weapon);
        }
        ChangeInventory?.Invoke(_inventory);
        Destroy(takedItem.gameObject);
    }

    public void DropItem(ItemData dropedItem)
    {
        var dropItem = Instantiate(dropedItem.Prefab, transform.position, Quaternion.identity);
        dropItem.transform.GetComponent<Item>().AddCount(_inventory[dropedItem]);
        if (dropItem.GetComponent<Weapon>())
        {
            dropItem.TryGetComponent<Weapon>(out Weapon dropWeapon);
            dropWeapon.AddCountAmmo(_weaponHandled.CurrentWeapon.CountAmmo);
        }
        _inventory.Remove(dropedItem);
        ChangeInventory?.Invoke(_inventory);
    }

    public void UseItem(ItemData usingItem)
    {
        _inventory.Remove(usingItem);
        ChangeInventory?.Invoke(_inventory);
    }

    public int TakeCountAmmoForReload(AmmoBox Ammo, int MaxCapacityAmmo)
    {
        foreach (var item in _inventory)
        {
            if(item.Key.Prefab.GetComponent<AmmoBox>() == Ammo)
            {
                if (MaxCapacityAmmo <= item.Value)
                {
                    _inventory[item.Key] = item.Value - MaxCapacityAmmo;
                    return MaxCapacityAmmo;
                }
                else if (MaxCapacityAmmo > item.Value)
                {
                    MaxCapacityAmmo = item.Value;
                    _inventory[item.Key] = item.Value - item.Value;
                    return MaxCapacityAmmo;
                }
            }
        }
        return 0;
    }

    public void RestockItems()
    {
        Dictionary<ItemData, int> _tempIitems = new Dictionary<ItemData, int>();
        foreach (var item in _inventory)
        {
            if (item.Value == 0)
                continue;
            else
                _tempIitems.Add(item.Key, item.Value);
        }
        _inventory = _tempIitems;
        ChangeInventory?.Invoke(_inventory);
    }

}
