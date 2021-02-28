using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GateHangar : Door
{
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        ClosedPosition = transform.localRotation.eulerAngles;
    }
    public override void OpenDoor(bool doorIsOpen)
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

    private void TransformHorisontalDoor(Vector3 to)
    {
        transform.DOLocalRotate(to, 8f);
    }
}
