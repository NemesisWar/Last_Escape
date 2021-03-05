using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorHorizontal : Door
{
    private float _distanceBetweenOpenAndClosed = 2f;

    private void Start()
    {
        ClosedPosition = transform.localPosition;
        OpenPosition = new Vector3(ClosedPosition.x - _distanceBetweenOpenAndClosed, ClosedPosition.y, ClosedPosition.z);
    }

    public override void OpenDoor(bool doorIsOpen)
    {
        if (AutoClose)
        {
            AutoCloseDoor(ClosedPosition, OpenPosition);
        }
        else if (!AutoClose)
        {
            if (doorIsOpen == true)
            {
                
                PlaySound(CloseAudio);
                TransformDoor(ClosedPosition);
            }
            else
            {
                PlaySound(OpenAudio);
                TransformDoor(OpenPosition);
            }
        }
    }

    protected override void TransformDoor(Vector3 newDoorPosition)
    {
        transform.DOLocalMove(newDoorPosition, 2f);
    }

    protected override void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }

    private void AutoCloseDoor(Vector3 from, Vector3 to)
    {
        Sequence sequence = DOTween.Sequence();
        PlaySound(OpenAudio);
        sequence.Append(transform.DOLocalMove(to, 2f));
        PlaySound(CloseAudio);
        sequence.Append(transform.DOLocalMove(from, 2f).SetDelay(5f));
    }
}
