using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotVisibleTransition : Transitions
{
    [SerializeField] private float _distanceOffset;

    private void Update()
    {
        if (Enemy.FoundTarget.PlayerFound == false && Vector3.Distance(transform.position, Enemy.FoundTarget.LastVisiblePlayerPosition)<_distanceOffset)
        {
            NeedTransit = true;
        }
    }
}
