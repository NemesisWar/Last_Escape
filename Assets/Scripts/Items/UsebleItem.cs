using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsebleItem : MonoBehaviour
{
    public float AddHealth => _addHealth;
    public float AddStamina => _addStamina;

    [SerializeField] private float _addHealth;
    [SerializeField] private float _addStamina;

    public void Use(Player player)
    {
        if (AddStamina > 0)
            player.AddStamina(AddStamina);
        if (AddHealth > 0)
            player.AddHealth(AddHealth);
    }
}
