using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSomeComponentsAfterDeath : MonoBehaviour
{
    private MonoBehaviour[] _scripts;

    void Start()
    {
        MonoBehaviour[] _scripst = GetComponents<MonoBehaviour>();
        foreach (var script in _scripst)
        {
            script.enabled = false;
        }
    }
}
