using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoPanel_UI : MonoBehaviour
{
    [SerializeField] private WeaponHandled _weaponHandled;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _countAmmo;

    private Weapon _weapon;

    private void Start()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
    }

    private void OnEnable()
    {
        _weaponHandled.WeaponHasBeenChanged += OnWeaponHasBeenChanged;
    }

    private void OnDisable()
    {
        _weaponHandled.WeaponHasBeenChanged -= OnWeaponHasBeenChanged;
    }

    private void OnWeaponHasBeenChanged(Weapon weapon)
    {
        if (weapon.Ammo==null)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
            _weapon = null;
            return;
        }
        _image.sprite = weapon.Ammo.Icon;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        _weapon = weapon;
    }

    private void Update()
    {
        if (_weapon==null)
        {
            _countAmmo.text = null;
            return;
        }
        _countAmmo.text = _weapon.CountAmmo.ToString();
    }



}
