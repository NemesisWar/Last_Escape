using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private Transform _dragingParent;
    private Image _image;
    private Transform _prevoiusParent;

    public void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
        _prevoiusParent = transform.parent;
        transform.parent = _dragingParent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _image.raycastTarget = true;
        if(eventData.pointerCurrentRaycast.gameObject.name == "GamePanel")
        {
            _playerInventory.DropItem(GetComponent<InventoryCell>().Item);
        }
        else if(eventData.pointerCurrentRaycast.gameObject.name == "ActiveItemImage" && GetComponent<InventoryCell>().Item.UsibleItem)
        {
            eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ActiveItem activeItem);
            activeItem.SetItem(GetComponent<InventoryCell>().Item);
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "ActiveKeyImage" && GetComponent<InventoryCell>().Item.KeyItem)
        {
            eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ActiveKey activeKey);
            activeKey.SetKey(GetComponent<InventoryCell>().Item);
        }
        else if (eventData.pointerCurrentRaycast.gameObject.name == "WeaponImageImage" && GetComponent<InventoryCell>().Item.Prefab.GetComponent<Weapon>())
        {
            eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out ActiveWeapon activeWeapon);
            activeWeapon.SetWeapon(GetComponent<InventoryCell>().Item);
        }
        transform.parent = _prevoiusParent;
    }
}
