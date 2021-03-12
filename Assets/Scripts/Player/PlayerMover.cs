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
    private Vector2 _moveDistance;
    private float _previousSpeed;
    private float _verticalSpeed;
    private bool _previousJump;
    private Rigidbody _rigidbody;
    private Player _player;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        float speed = 0;
        float run = Input.GetAxis("Run") + 1;
        float axisX = Input.GetAxis("Horizontal");
        float axisZ = Input.GetAxis("Vertical");

        if (axisX != 0 || axisZ != 0)
        {
            float maxAxis = Mathf.Max(Mathf.Abs(axisX), Mathf.Abs(axisZ));
            if (run > 1 && _player.Stamina > _minStaminaForRun)
            {
                _player.SpendingStamina(_minStaminaForRun);
            }
            if (_player.Stamina <= _minStaminaForRun || run == 1f)
            {
                run = 1;
                _player.AddStamina(_regenetareStamina);
            }
            speed = maxAxis * _startSpeed * run;
        }
        else
        {
            _player.AddStamina(_regenetareStamina * 2f);
        }
        if (_previousSpeed != speed)
        {
            _previousSpeed = speed;
            SpeedHasBeenChanged?.Invoke(speed);
        }
        _moveDistance = new Vector2(axisX * _previousSpeed, axisZ * _previousSpeed);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        bool jump = Input.GetButton("Jump");
        Vector3 move = new Vector3(_moveDistance.x, 0, _moveDistance.y);
        move = Vector3.ClampMagnitude(move, _previousSpeed);
        move = transform.TransformDirection(move);
        bool hitGround = CheckGround();
        if (hitGround)
        {
            if (jump)
            {
                _verticalSpeed = _powerJump;
                _previousJump = jump;
                OnGroundStatusHasBeenChanged?.Invoke(jump);
            }
            else
            {
                _verticalSpeed = 0f;
                if (_previousJump != jump)
                {
                    _previousJump = jump;
                    OnGroundStatusHasBeenChanged?.Invoke(jump);
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
        move *= Time.deltaTime;
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
