using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [HideInInspector]
    public AmmoBox Ammo => AmmoBox;
    public int CountAmmo => _ammoInGun;
    public int MaxCountAmmo => _maxAmmoInGun;
    public Transform Lever => _lever;
    public Transform Forend => _forend;
    public string Label => _label;

    [SerializeField] protected Bullet Bullet;
    [SerializeField] protected AmmoBox AmmoBox;
    [SerializeField] protected Transform BulletSpawn;
    [SerializeField] protected AudioClip FireClip;
    [SerializeField] protected AudioClip ReloadedClip;
    [SerializeField] protected AudioClip EmptyClip;
    protected AudioSource AudioSource;
    protected bool ShootMade;
    protected float CurrentTime;

    [SerializeField] private ParticleSystem _fireball;
    [SerializeField] private int _maxAmmoInGun;
    [SerializeField] private int _ammoInGun;
    [SerializeField] private float _delayTime;
    [SerializeField] private Transform _lever;
    [SerializeField] private Transform _forend;
    [SerializeField] private string _label;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnValidate()
    {
        if (_ammoInGun >= _maxAmmoInGun)
            _ammoInGun = _maxAmmoInGun;
    }

    public void AddCountAmmo(int countAmmo)
    {
        _ammoInGun = countAmmo;
    }

    public void Reload(int count)
    {
        AudioSource.PlayOneShot(ReloadedClip);
        _ammoInGun = count;
    }

    public abstract void TryShoot();

    protected bool HaveAmmo()
    {
        return _ammoInGun > 0;
    }

    protected bool DelayTimePassed()
    {
        return CurrentTime >= _delayTime;
    }

    protected void Shoot()
    {
        CurrentTime = 0;
        Instantiate(Bullet, BulletSpawn.position, BulletSpawn.rotation);
        _fireball.Play();
        AudioSource.PlayOneShot(FireClip);
        ShootMade = true;
        _ammoInGun--;
    }

    protected void CanNotShoot()
    {
        CurrentTime = 0;
        AudioSource.PlayOneShot(EmptyClip);
    }
}
