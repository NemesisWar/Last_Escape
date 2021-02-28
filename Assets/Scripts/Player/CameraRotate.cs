using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRotate : MonoBehaviour
{
    public Camera Сamera { get; private set; }

    [SerializeField] private Transform _target;
    [SerializeField] private float _minAxisX;
    [SerializeField] private float _maxAxisX;
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _rotationX;
    [SerializeField] private float _rotationY;
    [SerializeField] private Vector3 _distanceToTarget;
    private float _inputHorisontal;
    private float _inputVertical;

    private void Awake()
    {
        Сamera = GetComponent<Camera>();
        _distanceToTarget = Сamera.transform.position - _target.position;
        _rotationX = transform.eulerAngles.y;
        _rotationY = transform.eulerAngles.x;
    }

    private void LateUpdate()
    {
        _inputHorisontal = Input.GetAxis("Mouse X");
        _inputVertical = Input.GetAxis("Mouse Y");
        if (_inputHorisontal != 0)
        {
            _rotationY += _inputHorisontal * _speedRotation * Time.deltaTime;
        }
        if (_inputVertical != 0)
        {
            _rotationX += _inputVertical * _speedRotation * Time.deltaTime;
            _rotationX = Mathf.Clamp(_rotationX, _minAxisX, _maxAxisX);
        }
        Quaternion rootation = Quaternion.Euler(-_rotationX, _rotationY, 0);
        transform.position = _target.position - (rootation * _distanceToTarget);
        transform.LookAt(_target);
    }
}
