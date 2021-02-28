using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    [SerializeField] private WeaponHandled _weaponHandled;
    [SerializeField] private PlayerInventory _playerInventory;
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
