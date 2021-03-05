using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateHangar : Door
{
    private void Start()
    {
        ClosedPosition = transform.localRotation.eulerAngles;
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
        transform.DOLocalRotate(newDoorPosition, 8f);
    }

    protected override void PlaySound(AudioClip audioClip)
    {
        AudioSource.PlayOneShot(audioClip);
    }
}
