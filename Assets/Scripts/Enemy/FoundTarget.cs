using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundTarget : MonoBehaviour
{
    public bool PlayerFound => _playerFound;
    public Vector3 LastVisiblePlayerPosition => _lastVisiblePlayerPosition;
    public Vector3 Offset;

    [SerializeField] private List<Vector3> _points;
    [SerializeField] private int _raysCount;
    [SerializeField] private float _angle;
    [SerializeField] private float _distanceRays;
    private bool _playerFound;
    private Vector3 _lastVisiblePlayerPosition;
    private Quaternion _originalPos;
    private Player _player;
    private Enemy _enemy;
    private float _angleBetweenPlayerAndEnemy;
    private float _distanceBetweenPlayerAndEnemy;
    private Vector3 _playerPosition;

    private void Start()
    {
        _originalPos = transform.localRotation;
        _player = GetComponentInParent<Enemy>().Target;
        _enemy = GetComponentInParent<Enemy>();
    }

    private void FixedUpdate()
    {
        _playerPosition = _player.transform.position - transform.position;
        _angleBetweenPlayerAndEnemy = Vector3.Angle(_playerPosition, _enemy.transform.forward);
        _distanceBetweenPlayerAndEnemy = _playerPosition.magnitude;
        if((_angleBetweenPlayerAndEnemy<=_angle && _distanceBetweenPlayerAndEnemy <=_distanceRays) || _playerFound == true)
        {
            transform.LookAt(_player.transform.position);
            _playerFound = false;
            CreatePointsForRays();
            GetRaycasts();
        }
        else
        {
            transform.localRotation = _originalPos;
        }
    }


    private void CreatePointsForRays()
    {
        _points.Clear();
        float j = 0;
        for (int i = 0; i < _raysCount; i++)
        {
            var x = Mathf.Sin(j);
            var y = Mathf.Cos(j);
            j += _angle * Mathf.Deg2Rad / _raysCount;
            Vector3 dir = transform.TransformDirection(new Vector3(0, x, y));
            if (x != 0)
            {
                _points.Add(dir);
                dir = transform.TransformDirection(new Vector3(0, -x, y));
            }
            _points.Add(dir);
        }
    }

    private void GetRaycasts()
    {
        transform.LookAt(_player.transform);
        foreach (var point in _points)
        {
            RaycastHit hit = new RaycastHit();
            Vector3 pos = transform.position + Offset;
            if (Physics.Raycast(pos, point, out hit, _distanceRays))
            {
                if (hit.transform.TryGetComponent(out Player player))
                {
                    Debug.DrawLine(pos, hit.point, Color.green);
                    _playerFound = true;
                    _lastVisiblePlayerPosition = hit.point;
                    transform.LookAt(player.transform);
                }
                else
                {
                    Debug.DrawLine(pos, hit.point, Color.blue);
                }
            }
            else
            {
                Debug.DrawRay(pos, point * _distanceRays, Color.red);

            }
        }
    }
}
