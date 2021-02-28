using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayerCamera : MonoBehaviour
{
    public void OnEnable()
    {
        gameObject.SetActive(true);
    }

    public void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
