using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class ActiveWeapon : MonoBehaviour
{
    public event UnityAction<Weapon> WeaponChanged;

    [SerializeField] private Image _image;
    [SerializeField] private ItemData _item;

    private void Start()
    {
        _image = GetComponent<Image>();
        SetWeapon();
    }

    public void SetWeapon(ItemData item)
    {
        _item = item;
        _image.sprite = _item.ImageItem;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        WeaponChanged?.Invoke(_item.Prefab.GetComponent<Weapon>());
    }

    public void CheckWeaponInInventory(Dictionary<ItemData, int> items)
    {
        foreach (var item in items)
        {
            if (item.Key == _item)
            {
                SetWeapon(_item);
                return;
            }
        }
        SetWeapon();
    }
    private void SetWeapon()
    {
        _item = null;
        _image.sprite = null;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        WeaponChanged?.Invoke(null);
    }
}
