using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public string Label => _label;
    public Sprite Icon => _icon;

    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
}
