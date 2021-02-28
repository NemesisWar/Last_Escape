using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public int Count => _count;
    public ItemData ItemData => _itemData;

    [SerializeField] private ItemData _itemData;
    [SerializeField] private bool _isWeapon;
    [SerializeField] private int _count;

    private void Start()
    {
        _isWeapon = GetComponent<Weapon>();
    }

    public void AddCount(int count)
    {
        _count = count;
    }
}
