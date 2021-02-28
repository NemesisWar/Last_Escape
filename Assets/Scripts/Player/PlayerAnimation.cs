using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(PlayerShooting))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _jump;
    [SerializeField] private bool _readyToShoot;
    [SerializeField] private bool _weaponOnHand;
    [SerializeField] private bool _playerDie;
    [SerializeField] private Weapon _currentWeapon = null;
    private Player _player;
    private PlayerMover _playerMover;
    private PlayerShooting _playerShooting;
    private WeaponHandled _weaponHandled;
    private Animator _animator;

    private void Start()
    {
        _player = GetComponent<Player>();
        _playerMover = GetComponent<PlayerMover>();
        _playerShooting = GetComponent<PlayerShooting>();
        _animator = GetComponent<Animator>();
        _player.Dying += OnPlayerDying;
        _playerMover.SpeedHasBeenChanged += ChangeSpeedValue;
        _playerMover.OnGroundStatusHasBeenChanged += ChangeGroundStatus;
        _playerShooting.ReadyToShoot += ChangeReadyToShootStatus;
        _weaponHandled = GetComponentInChildren<WeaponHandled>();
        _weaponHandled.WeaponHasBeenChanged += ChangeWeapon;
    }

    private void OnDisable()
    {
        _weaponHandled.WeaponHasBeenChanged -= ChangeWeapon;
        _player.Dying -= OnPlayerDying;
        _playerMover.SpeedHasBeenChanged -= ChangeSpeedValue;
        _playerMover.OnGroundStatusHasBeenChanged -= ChangeGroundStatus;
        _playerShooting.ReadyToShoot -= ChangeReadyToShootStatus;
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _weaponOnHand = true;
        _currentWeapon = weapon;
        if (weapon == null || weapon.Label == "melee")
        {
            _weaponOnHand = false;
        }
        _animator.SetBool("WeaponOnHand", _weaponOnHand);
    }

    private void OnPlayerDying()
    {
        _playerDie = true;
        _animator.SetBool("Dead", _playerDie);
    }

    private void ChangeSpeedValue(float speed)
    {
        _speed = speed;
        _animator.SetFloat("Speed", _speed);
    }

    private void ChangeGroundStatus(bool onGround)
    {
        _jump = onGround;
        _animator.SetBool("Jump", _jump);
    }

    private void ChangeReadyToShootStatus(bool readyToShoot)
    {
        _readyToShoot = readyToShoot;
        _animator.SetBool("ReadyToShoot", _readyToShoot);
    }
}
