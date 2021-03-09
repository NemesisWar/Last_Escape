using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerRotate))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Player))]
public class PlayerMover : MonoBehaviour
{
    public event UnityAction<float> SpeedHasBeenChanged;
    public event UnityAction<bool> OnGroundStatusHasBeenChanged;

    [SerializeField] private float _MaxVerticalSpeed;
    [SerializeField] private float _startSpeed;
    [SerializeField] private float _powerJump;
    [SerializeField] private float _minStaminaForRun;
    [SerializeField] private float _regenetareStamina;
    [SerializeField] private float _distanceRay;
    private float _speed;
    private float _previousSpeed;
    private float _axisX;
    private float _axisZ;
    private float _verticalSpeed;
    private bool _jump;
    private bool _previousJump;
    private float _run;
    private Rigidbody _rigidbody;
    private Player _player;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _speed = 0;
        _axisX = Input.GetAxis("Horizontal");
        _axisZ = Input.GetAxis("Vertical");
        _jump = Input.GetButton("Jump");
        _run = Input.GetAxis("Run") + 1;
        if (_axisX != 0 || _axisZ != 0)
        {
            float maxAxis = Mathf.Max(Mathf.Abs(_axisX), Mathf.Abs(_axisZ));
            if (_run > 1 && _player.Stamina > _minStaminaForRun)
            {
                _player.SpendingStamina(_minStaminaForRun);
            }
            if (_player.Stamina <= _minStaminaForRun || _run == 1f)
            {
                _run = 1;
                _player.AddStamina(_regenetareStamina);
            }
            _speed = maxAxis * _startSpeed * _run;
        }
        else
        {
            _player.AddStamina(_regenetareStamina * 2f);
        }
        if (_previousSpeed != _speed)
        {
            _previousSpeed = _speed;
            SpeedHasBeenChanged?.Invoke(_speed);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        Vector3 move = new Vector3(_axisX * _speed, 0, _axisZ * _speed);
        move = Vector3.ClampMagnitude(move, _speed);
        move = transform.TransformDirection(move);
        bool hitGround = CheckGround();
        if (hitGround)
        {
            if (_jump)
            {
                _verticalSpeed = _powerJump;
                _previousJump = _jump;
                OnGroundStatusHasBeenChanged?.Invoke(_jump);
            }
            else
            {
                _verticalSpeed = 0f;
                if (_previousJump != _jump)
                {
                    _previousJump = _jump;
                    OnGroundStatusHasBeenChanged?.Invoke(_jump);
                }
            }
        }
        else
        {
            _verticalSpeed += Physics.gravity.y * 3 * Time.deltaTime;
            if (_verticalSpeed < _MaxVerticalSpeed)
            {
                _verticalSpeed = _MaxVerticalSpeed;
            }
        }
        move.y = _verticalSpeed;
        move = move * Time.deltaTime;
        move += transform.position;
        _rigidbody.MovePosition(move);
    }

    private bool CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Physics.Raycast(ray, out RaycastHit hit);
        return _distanceRay >= hit.distance;
    }
}
