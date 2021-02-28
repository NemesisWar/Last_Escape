using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Door : MonoBehaviour
{
    [SerializeField] protected bool AutoClose;
    [SerializeField] protected Vector3 ClosedPosition;
    [SerializeField] protected Vector3 OpenPosition;
    [SerializeField] protected AudioClip OpenAudio;
    [SerializeField] protected AudioClip CloseAudio;
    protected AudioSource AudioSource;

    public abstract void OpenDoor(bool positionDoor);
}
