using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaus_Gun : Weapon
{
    public override void Reload(int count)
    {
        AudioSource.PlayOneShot(ReloadedClip);
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
