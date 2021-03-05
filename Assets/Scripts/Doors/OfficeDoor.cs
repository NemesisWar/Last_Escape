using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OfficeDoor : Door
{
    private void Start()
    {
        ClosedPosition = transform.eulerAngles;
        OpenPosition = new Vector3(ClosedPosition.x, ClosedPosition.y + 120f, ClosedPosition.z);
    }

    public override void OpenDoor(bool doorIsOpen)
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

    protected override void TransformDoor(Vector3 newDoorPosition)
    {
        transform.DORotate(newDoorPosition, 2f);
    }

    protected override void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
