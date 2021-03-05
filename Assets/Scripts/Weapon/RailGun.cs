using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : Weapon
{
    [SerializeField] private ParticleSystem _idleEffect;
    [SerializeField] private AudioClip _charging;

    public override void TryShoot()
    {
        
        if (HaveAmmo())
        {
            PlayIdleEffect();
            if (DelayTimePassed())
            {
                Shoot();
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

    private void PlayIdleEffect()
    {
        _idleEffect.Play();
        if (AudioSource.isPlaying == false)
        {
            AudioSource.PlayOneShot(_charging);
        }
        CurrentTime += Time.deltaTime;
    }
}
