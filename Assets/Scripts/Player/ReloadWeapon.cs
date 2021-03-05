using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInventory))]
public class ReloadWeapon : MonoBehaviour
{
    private WeaponHandled _weaponHandled;
    private PlayerInventory _playerInventory;
    private int _countAmmoForReload;

    private void Start()
    {
        _weaponHandled = GetComponentInChildren<WeaponHandled>();
        _playerInventory = GetComponent<PlayerInventory>();
        _countAmmoForReload = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _countAmmoForReload =_playerInventory.TakeCountAmmoForReload(_weaponHandled.CurrentWeapon.Ammo, _weaponHandled.CurrentWeapon.MaxCountAmmo);
            if (_countAmmoForReload == 0)
                return;
            else
            {
                _weaponHandled.CurrentWeapon.Reload(_countAmmoForReload);
                _playerInventory.RestockItems();
            }
        }
    }
}
