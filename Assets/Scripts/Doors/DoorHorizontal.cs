using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorHorizontal : Door
{
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        ClosedPosition = transform.localPosition;
        OpenPosition = new Vector3(ClosedPosition.x - 2, ClosedPosition.y, ClosedPosition.z);
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
        transform.DOLocalMove(to, 2f);
    }

    private void AutoCloseDoor(Vector3 from, Vector3 to)
    {
        Sequence sequence = DOTween.Sequence();
        AudioSource.PlayOneShot(OpenAudio);
        sequence.Append(transform.DOLocalMove(to, 2f));
        AudioSource.PlayOneShot(CloseAudio);
        sequence.Append(transform.DOLocalMove(from, 2f).SetDelay(5f));
    }
}
