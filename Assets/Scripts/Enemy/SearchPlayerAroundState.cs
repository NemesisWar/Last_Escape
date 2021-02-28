using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SearchPlayerAroundState : State
{
    private void OnEnable()
    {
        transform.DORotate(new Vector3(0,360,0), 2f, RotateMode.FastBeyond360);
    }
}
