using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishPoint : MonoBehaviour
{
    public event UnityAction GameFinished;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            GameFinished?.Invoke();
            Time.timeScale = 0;
        }
    }
}
