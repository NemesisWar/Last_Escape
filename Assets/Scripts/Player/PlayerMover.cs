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
    private float _speed;
    private float _previousSpeed;
    private float _axisX;
    private float _axisZ;
    private float _verticalSpeed;
    private bool _jump;
    private bool _previousJump;
    private float _run;
    private bool _hitGround;
    private Vector3 _move;
    private Rigidbody _rigidbody;
    private Player _player;
    private Ray _ray;
    private RaycastHit _hit;
    private float _distanceRay = 0.1f;
    private float _maxAxis;
   
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
        _run = Input.GetAxis("Run")+1;
        if (_axisX != 0 || _axisZ != 0)
        {
            _maxAxis = Mathf.Max(Mathf.Abs(_axisX), Mathf.Abs(_axisZ));
            if(_run > 1 && _player.Stamina > _minStaminaForRun)
            {
                _player.SpendingStamina(_minStaminaForRun);
            }
            if(_player.Stamina <= _minStaminaForRun || _run==1f)
            {
                _run = 1;
                _player.AddStamina(_regenetareStamina);
            }
            _speed = _maxAxis * _startSpeed * _run;
        }
        else
        {
            _player.AddStamina(_regenetareStamina*2f);
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
        _move = new Vector3(0,0,0);
        _move.x = _axisX * _speed;
        _move.z = _axisZ * _speed;
        _move = Vector3.ClampMagnitude(_move, _speed);
        _move = transform.TransformDirection(_move);
        _hitGround = false;
        _ray = new Ray(transform.position, Vector3.down);

        CheckGround();
        if (_hitGround)
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
                if(_previousJump != _jump)
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

        _move.y = _verticalSpeed;
        _move = _move * Time.deltaTime;
        _move += transform.position;
        _rigidbody.MovePosition(_move);
    }

    private void CheckGround()
    {
        if (Physics.Raycast(_ray, out _hit))
        {
            _hitGround = _distanceRay >= _hit.distance;
        }
    }
}
