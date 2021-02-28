using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DitzelGames.FastIK;

public class WeaponHandled : MonoBehaviour
{
    public event UnityAction<Weapon> WeaponHasBeenChanged;
    public Weapon CurrentWeapon => _currentWeapon;

    [SerializeField] private ActiveWeapon _activeWeapon;
    [SerializeField] private FastIKFabric _leftHand;
    [SerializeField] private FastIKFabric _rightHand;
    private Weapon[] _weapons;
    [SerializeField] private Weapon _currentWeapon;

    private void OnEnable()
    {
        _activeWeapon.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _activeWeapon.WeaponChanged -= OnWeaponChanged;
    }

    private void Awake()
    {
        _weapons = GetComponentsInChildren<Weapon>();
        HideAllWeapons();
        _weapons[0].transform.gameObject.SetActive(true);
        _currentWeapon = _weapons[0];
    }

    private void HideAllWeapons()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].transform.gameObject.SetActive(false);
        }
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        if (weapon == null)
        {
            OnWeaponChanged();
            return;
        }
        if(_currentWeapon.Label == weapon.Label)
        {
            return;
        }
        for (int i = 0; i < _weapons.Length; i++)
        {
            HideAllWeapons();
            if (_weapons[i].Label == weapon.Label)
            {
                _currentWeapon = _weapons[i];
                _weapons[i].transform.gameObject.SetActive(true);
                WeaponHasBeenChanged?.Invoke(_currentWeapon);
                SetTargetForIKAnimation(_currentWeapon.Forend, _currentWeapon.Lever);
                break;
            }
        }
    }

    public void AddAmmoInWeapon(Weapon weapon)
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i].Label == weapon.Label)
            {
                _weapons[i].AddCountAmmo(weapon.CountAmmo);
                break;
            }
        }
    }

    private void OnWeaponChanged()
    {
        HideAllWeapons();
        _currentWeapon = _weapons[0];
        _weapons[0].transform.gameObject.SetActive(true);
        WeaponHasBeenChanged?.Invoke(_currentWeapon);
        SetTargetForIKAnimation();
    }

    private void SetTargetForIKAnimation(Transform forend, Transform lever)
    {
        _leftHand.Target = forend;
        _rightHand.Target = lever;
        _leftHand.enabled = true;
        _rightHand.enabled = true;
    }
    private void SetTargetForIKAnimation()
    {
        _leftHand.Target = null;
        _rightHand.Target = null;
        _leftHand.enabled = false;
        _rightHand.enabled = false;
    }
}
