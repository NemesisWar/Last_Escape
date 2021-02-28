using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Lock : MonoBehaviour
{
    [SerializeField] private List <Door> _doors;
    [SerializeField] private int _securutyLevel;
    [SerializeField] private AudioClip _accessGranted;
    [SerializeField] private AudioClip _accessDenied;
    private bool _isOpen;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void TryOpen(int keyLevel)
    {
        if (keyLevel >= _securutyLevel)
        {
            _audioSource.PlayOneShot(_accessGranted);
            foreach (var door in _doors)
            {
                door.OpenDoor(_isOpen);
            }
            _isOpen = !_isOpen;
        }
        else
        {
            _audioSource.PlayOneShot(_accessDenied);
        }
    }
}
