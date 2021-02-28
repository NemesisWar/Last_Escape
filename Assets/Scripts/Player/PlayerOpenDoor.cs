using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpenDoor : MonoBehaviour
{
    [SerializeField] private ActiveKey _activeKeyInInventory;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (other.TryGetComponent(out Lock lockDoor))
            {
                lockDoor.TryOpen(_activeKeyInInventory.KeyLevel);
            }
        }
    }
}
