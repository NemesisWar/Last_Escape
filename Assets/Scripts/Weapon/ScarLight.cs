using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarLight : Weapon
{
    public override void TryShoot()
    {
        if (DelayTimePassed())
        {
            if (HaveAmmo())
            {
                Shoot();
            }
            else
            {
                CanNotShoot();
            }
        }
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
    }
}
