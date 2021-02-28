using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerMover))]
public class PlayerRotate : MonoBehaviour
{
    private CameraRotate _cameraRotate;
    private PlayerMover _playerMover;
    private Quaternion _cameraPosition;
    private Quaternion _playerPosition;

    private void Start()
    {
        _cameraRotate = GetComponentInChildren<CameraRotate>();
        _playerMover = GetComponent<PlayerMover>();
    }

    private void LateUpdate()
    {
        _cameraPosition = _cameraRotate.Сamera.transform.rotation;
        _playerPosition = _playerMover.transform.rotation;
        _cameraPosition.eulerAngles = new Vector3(0, _cameraPosition.eulerAngles.y, 0);
        _playerMover.transform.rotation = Quaternion.Lerp(_playerPosition, _cameraPosition, 5f * Time.deltaTime);
    }
}
