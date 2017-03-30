using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float Speed = 100f;

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

        _rigidbody.velocity = transform.up * Speed;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}
