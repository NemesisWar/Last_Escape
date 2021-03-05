using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XM01 : Weapon
{
    //public override void TryShoot()
    //{
    //    if (CurrentTime >= DelayTime)
    //    {
    //        CurrentTime = 0;
    //        if (AmmoInGun > 0)
    //        {
    //            Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
    //            Fireball.Play();
    //            AudioSource.PlayOneShot(FireClip);
    //            ShootMade = true;
    //            AmmoInGun--;
    //        }
    //        else
    //        {
    //            AudioSource.PlayOneShot(EmptyClip);
    //        }
    //    }
    //}

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
        ShootMade = false;
        CurrentTime += Time.deltaTime;
    }
}
