using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkTransition : Transitions
{
    private void Update()
    {
        NeedTransit = true;
    }
}
