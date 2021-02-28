using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ActiveKey : MonoBehaviour
{
    public int KeyLevel => _keyLevel;

    [SerializeField] private Image _image;
    [SerializeField] private ItemData _item;
    [SerializeField] private int _keyLevel;

    private void Start()
    {
        _image = GetComponent<Image>();
        SetKey();
    }

    public void SetKey(ItemData item)
    {
        _item = item;
        _image.sprite = _item.ImageItem;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        _keyLevel = item.Prefab.GetComponent<Key>().GetLevel();
    }

    public void CheckKeyInInventory(Dictionary<ItemData, int> items)
    {
        foreach (var item in items)
        {
            if (item.Key == _item)
            {
                SetKey(_item);
                return;
            }
        }
        SetKey();
    }
    private void SetKey()
    {
        _item = null;
        _image.sprite = null;
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0f);
        _keyLevel = 0;
    }
}
