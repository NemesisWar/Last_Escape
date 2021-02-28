using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private int _keyLevel;

    public int GetLevel()
    {
        return _keyLevel;
    }
}
