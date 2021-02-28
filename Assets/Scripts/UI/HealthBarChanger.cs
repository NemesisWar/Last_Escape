using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarChanger : MonoBehaviour
{
    [SerializeField] private float _pathTime;

    private float _pathRunningTime;
    private Slider _slider;
    private Player _player;
    private Coroutine _coroutine;
    private float _prevousValue;
    private float _value;

    private void OnEnable()
    {
        _slider = GetComponent<Slider>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartValue(_player.Health);
        _player.ChangeHealth += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.ChangeHealth -= OnValueChanged;
    }

    private void StartValue(float Value)
    {
        _slider.value = Value;
    }

    private void OnValueChanged()
    {
        ChangeValue(_player.Health);
    }

    private void ChangeValue(float Value)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _prevousValue = _slider.value;
        _value = Value;
        _coroutine = StartCoroutine(LerpChangeHealthBar(_value, _prevousValue));
    }

    private IEnumerator LerpChangeHealthBar(float Value, float PriviousValue)
    {
        _pathRunningTime = _pathTime;
        while (_slider.value != _value)
        {
            _pathRunningTime -= Time.deltaTime;
            _slider.value = Mathf.Lerp(Value, PriviousValue, _pathRunningTime / _pathTime);
            yield return null;
        }
        StopValueChanged(_coroutine);
    }

    private void StopValueChanged(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
        _coroutine = null;
    }
}
