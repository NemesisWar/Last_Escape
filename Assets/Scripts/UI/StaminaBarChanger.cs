using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarChanger : MonoBehaviour
{
    private Slider _slider;
    private Player _player;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartValue(_player.Stamina);
        _player.ChangeStamina += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.ChangeStamina -= OnValueChanged;
    }
    private void OnValueChanged()
    {
        ChangeValue(_player.Stamina);
    }

    private void StartValue(float Value)
    {
        _slider.value = Value;
    }

    private void ChangeValue(float Value)
    {
        _slider.value = Value;
    }
}
