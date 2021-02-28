using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Weapon : MonoBehaviour
{
    [HideInInspector]
    public AmmoBox Ammo => AmmoBox;
    public int CountAmmo => AmmoInGun;
    public int MaxCountAmmo => MaxAmmoInGun;
    public Transform Lever => _lever;
    public Transform Forend => _forend;
    public string Label => _label;

    [SerializeField] private Transform _lever;
    [SerializeField] private Transform _forend;
    [SerializeField] private string _label;

    [SerializeField] protected Bullet Bullet;
    [SerializeField] protected AmmoBox AmmoBox;
    [SerializeField] protected Transform BulletSpawn;
    [SerializeField] protected float DelayTime;
    [SerializeField] protected int MaxAmmoInGun;
    [SerializeField] protected int AmmoInGun;
    [SerializeField] protected AudioClip FireClip;
    [SerializeField] protected AudioClip ReloadedClip;
    [SerializeField] protected AudioClip EmptyClip;
    [SerializeField] protected ParticleSystem Fireball;
    protected AudioSource AudioSource;
    protected bool ShootMade;
    protected float CurrentTime;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnValidate()
    {
        if (AmmoInGun >= MaxAmmoInGun)
            AmmoInGun = MaxAmmoInGun;
    }
    public void AddCountAmmo(int countAmmo)
    {
        AmmoInGun = countAmmo;
    }

    public abstract void Shoot();
    public abstract void Reload(int count);
}
