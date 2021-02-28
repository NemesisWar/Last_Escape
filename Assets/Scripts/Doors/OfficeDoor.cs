using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OfficeDoor : Door
{
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        ClosedPosition = transform.eulerAngles;
        OpenPosition = new Vector3(ClosedPosition.x, ClosedPosition.y + 120f, ClosedPosition.z);
    }

    public override void OpenDoor(bool doorIsOpen)
    {
        if (doorIsOpen == true)
        {
            AudioSource.PlayOneShot(CloseAudio);
            TransformRotationDoor(ClosedPosition);
        }
        else
        {
            AudioSource.PlayOneShot(OpenAudio);
            TransformRotationDoor(OpenPosition);
        }
    }

    private void TransformRotationDoor(Vector3 to)
    {
        transform.DORotate(to, 2f);
    }
}
