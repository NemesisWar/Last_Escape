using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UseFirstAidKit : MonoBehaviour
{
    [SerializeField] private UnityEvent _tryUsingItem;

    public event UnityAction TryUsingItem
    {
        add => _tryUsingItem.AddListener(value);
        remove => _tryUsingItem.RemoveListener(value);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            _tryUsingItem?.Invoke();
        }
    }
}
