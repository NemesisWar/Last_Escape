using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorVertical : Door
{
    private void Start()
    {
        ClosedPosition = transform.localPosition;
    }

    public override void OpenDoor(bool doorIsOpen)
    {
        if (!AutoClose)
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
        transform.DOLocalMove(newDoorPosition, 5f);
    }

    protected override void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
