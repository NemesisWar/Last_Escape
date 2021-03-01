using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rail_Gun : Weapon
{
    [SerializeField] private ParticleSystem _idleEffect;
    [SerializeField] private AudioClip _charging;

    public override void Reload(int count)
    {
        AudioSource.PlayOneShot(ReloadedClip);
        AmmoInGun = count;
    }

    public override void Shoot()
    {
        if (AmmoInGun > 0)
        {
            _idleEffect.Play();
            if (Input.GetButton("Fire1"))
            {
                if (AudioSource.isPlaying == false)
                {
                    AudioSource.PlayOneShot(_charging);
                }
                CurrentTime += Time.deltaTime;
                if (CurrentTime >= DelayTime)
                {
                    CurrentTime = 0;
                    Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
                    Fireball.Play();
                    AudioSource.PlayOneShot(FireClip);
                    AmmoInGun--;
                }
            }
        }
        else
        {
            _idleEffect.Stop();
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") == false && CurrentTime > 0)
        {
            AudioSource.Stop();
            CurrentTime = 0;
        }
    }
}
