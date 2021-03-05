using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player))
        {
            player.TakeDamage(player.Health);
        }
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(enemy.Health);
        }
    }
}
