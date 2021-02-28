using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;

    public string Label => _label;
    public Sprite Icon => _icon;
}
