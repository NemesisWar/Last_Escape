using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Image))]
public class InventoryCell : MonoBehaviour
{
    public ItemData Item => _item;
    public Image Image => _image;

    private TMP_Text _textCount;
    private Image _image;
    private ItemData _item;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _textCount = GetComponentInChildren<TMP_Text>();
        _image.enabled = false;
    }

    public void ToAppointItem(ItemData appointedItem, int countAppointedItem)
    {
        _item = appointedItem;
        _image.enabled = true;
        _image.sprite = _item.ImageItem;
        if (countAppointedItem > 1)
            _textCount.text = countAppointedItem.ToString();
        else
            _textCount.text = null;
    }

    public void Clear()
    {
        _item = null;
        _image.enabled = false;
        _image.sprite = null;
        _textCount.text = null;
    }
}
