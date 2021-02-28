using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotVisibleTransition : Transitions
{
    [SerializeField] private float _distanceOffset;

    private void Update()
    {
        if (_enemy.FoundTarget.PlayerFound == false && Vector3.Distance(transform.position, _enemy.FoundTarget.LastVisiblePlayerPosition)<_distanceOffset)
        {
            NeedTransit = true;
        }
    }
}
