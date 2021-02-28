using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scar_L : Weapon
{
    public override void Reload(int count)
    {
        AmmoInGun = count;
    }

    public override void Shoot()
    {
        if (CurrentTime >= DelayTime)
        {
            CurrentTime = 0;
            if (AmmoInGun > 0)
            {
                Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                Fireball.Play();
                AudioSource.PlayOneShot(FireClip);
                AmmoInGun--;
            }
            else
            {
                AudioSource.PlayOneShot(EmptyClip);
            }
        }
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;
    }
}
