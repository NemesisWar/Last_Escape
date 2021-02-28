using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    private Vector3 _previousPosition;
    public int Damage => _damage;
    public float Speed => _speed;

    private void Start()
    {
        _previousPosition = transform.position;
        Destroy(gameObject, 5f);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Linecast(_previousPosition, transform.position, out hit))
        {
            if (hit.transform.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);
                Destroy(this.gameObject, 0.05f);
            }
        }
        _previousPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
            Destroy(this.gameObject, 0.05f);
        }
    }
}
