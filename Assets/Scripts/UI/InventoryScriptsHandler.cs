using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventoryScriptsHandler : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;

    private PlayerInventoryUI _playerInventoryUI;
    private ActiveItem _activeItem;
    private ActiveWeapon _activeWeapon;
    private ActiveKey _activeKey;

    private void Start()
    {
        Cursor.visible = false;
        _playerInventoryUI = GetComponentInChildren<PlayerInventoryUI>();
        _activeItem = GetComponentInChildren<ActiveItem>();
        _activeWeapon = GetComponentInChildren<ActiveWeapon>();
        _activeKey = GetComponentInChildren<ActiveKey>();
    }

    private void OnEnable()
    {
        _playerInventory.ChangeInventory +=OnChangeInventory;
    }

    private void OnDisable()
    {
        _playerInventory.ChangeInventory -= OnChangeInventory;
    }

    private void OnChangeInventory(Dictionary<ItemData, int> items)
    {
        _playerInventoryUI.AddItemsInCell(items);
        _activeWeapon.CheckWeaponInInventory(items);
        _activeItem.CheckItemInInventory(items);
        _activeKey.CheckKeyInInventory(items);
    }

    public void DisableEnablePanel()
    {
        if (gameObject.activeSelf==false)
        {
            gameObject.SetActive(true);
            _playerInventory.RestockItems();
            Cursor.visible = true;
        }
        else
        {
            gameObject.SetActive(false);
            Cursor.visible = false;
        }
    }
}
