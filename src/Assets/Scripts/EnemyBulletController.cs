using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private float _speed = 2f;
    private Rigidbody2D _rigidbody;
    private Vector2 _initialPlayerPosition;


    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _initialPlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void FixedUpdate()
    {
        if (_rigidbody == null) return;

        Vector3 direction = (_initialPlayerPosition - (Vector2)transform.position).normalized;
        transform.position += direction * _speed;

        if (transform.position.y > 20)
        {
            Destroy(gameObject);
        }
    }
}
