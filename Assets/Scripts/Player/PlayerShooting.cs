using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShooting : MonoBehaviour
{
    public event UnityAction<bool> ReadyToShoot;

    [SerializeField] private Transform _target;
    [SerializeField] private Transform _weaponPosition;
    [SerializeField] private float _leightLinecast;
    private bool _readyToShoot;
    private Transform _currentWeapon;
    private Transform _camera;
    private RaycastHit _hit;

    private void Start()
    {
        _camera = GetComponentInChildren<Camera>().transform;
        _currentWeapon = _weaponPosition.GetComponent<WeaponHandled>().CurrentWeapon.transform;
    }

    private void OnEnable()
    {
        _weaponPosition.GetComponent<WeaponHandled>().WeaponHasBeenChanged += OnWeaponHasBeenChanged;
    }
    private void OnDisable()
    {
        _weaponPosition.GetComponent<WeaponHandled>().WeaponHasBeenChanged -= OnWeaponHasBeenChanged;
    }

    private void OnWeaponHasBeenChanged(Weapon weapon)
    {
        _currentWeapon = _weaponPosition.GetComponent<WeaponHandled>().CurrentWeapon.transform;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _readyToShoot = !_readyToShoot;
            ReadyToShoot?.Invoke(_readyToShoot);
        }

        if (_readyToShoot)
        {
            var layermask = ~(1 << 8);
            Physics.Linecast(_camera.position, _camera.position + _camera.transform.forward * _leightLinecast, out _hit, layermask);
            Debug.DrawLine(_camera.position, _camera.position + _camera.transform.forward * _leightLinecast, Color.red);
            if (_hit.distance != 0)
            {
                _target.position = _hit.point;
            }

            else if (_hit.distance == 0)
            {
                _target.position = _camera.position + _camera.transform.forward * _leightLinecast;
            }
            Physics.Linecast(_currentWeapon.position, _target.position, out _hit);
            Debug.DrawLine(_currentWeapon.position, _target.position, Color.green);
            _currentWeapon.LookAt(_target);
            if (Input.GetButton("Fire1"))
            {
                if (_currentWeapon == null)
                {
                    _currentWeapon = _weaponPosition.GetComponent<WeaponHandled>().CurrentWeapon.transform;
                }
                _currentWeapon.GetComponent<Weapon>().Shoot();
            }
        }
    }
}
