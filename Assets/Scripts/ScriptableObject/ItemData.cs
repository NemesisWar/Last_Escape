using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    public string ItemName => _itemName;
    public GameObject Prefab => _prefabObject;
    public int IndexItem => _indexItem;
    public bool Stockable => _stockable;
    public bool UsibleItem => _usibleItem;
    public bool KeyItem => _keyItem;
    public Sprite ImageItem => _imageItem;

    [SerializeField] private int _indexItem;
    [SerializeField] private string _itemName;
    [SerializeField] private Sprite _imageItem;
    [SerializeField] private GameObject _prefabObject;
    [SerializeField] private bool _stockable;
    [SerializeField] private bool _usibleItem;
    [SerializeField] private bool _keyItem;
}
