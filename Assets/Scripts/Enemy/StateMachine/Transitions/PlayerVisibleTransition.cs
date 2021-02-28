using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibleTransition : Transitions
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _playerFound;
    private void Update()
    {
        if(_enemy.FoundTarget.PlayerFound == true && Vector3.Distance(transform.position, Target.transform.position) > 1.5f)
        {
            _audioSource.PlayOneShot(_playerFound);
            NeedTransit = true;
        }
    }

}
