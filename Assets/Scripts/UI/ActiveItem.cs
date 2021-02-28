using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ActiveItem : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private ItemData _item;

    private void Start()
    {
        _image = GetComponent<Image>();
        SetItem();
    }

    public void SetItem(ItemData item)
    {
        _item = item;
        _image.sprite = _item.ImageItem;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
    }
    public void CheckItemInInventory(Dictionary<ItemData, int> items)
    {
        foreach (var item in items)
        {
            if (item.Key == _item)
            {
                SetItem(_item);
                return;
            }
        }
        SetItem();
    }
    private void SetItem()
    {
        _item = null;
        _image.sprite = null;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
    }

    public void OnTryUsingItem(Player player)
    {
        if (_item == null)
            return;
        _item.Prefab.GetComponent<UsebleItem>().Use(player);
        player.GetComponent<PlayerInventory>().UseItem(_item);
    }
}
