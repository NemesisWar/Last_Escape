using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorVertical : Door
{
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        ClosedPosition = transform.localPosition;
    }
    public override void OpenDoor(bool doorIsOpen)
    {
        if (!AutoClose)
        {
            if (doorIsOpen == true)
            {
                AudioSource.PlayOneShot(CloseAudio);
                TransformHorisontalDoor(ClosedPosition);
            }

            else
            {
                AudioSource.PlayOneShot(OpenAudio);
                TransformHorisontalDoor(OpenPosition);
            }
        }
    }

    private void TransformHorisontalDoor(Vector3 to)
    {
        transform.DOLocalMove(to, 5f);
    }
}
