using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 5f;
    public float DistanceToTower = 10;
    public float ShotTime = 1f;

    private Rigidbody2D _rigidBody2D;
    private Transform _tower;
    private float _nextShot;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Transform>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Mathf.Abs(gameObject.transform.position.x - _tower.position.x) < DistanceToTower)
        {
            _rigidBody2D.velocity = new Vector2(0, 0);
            Shot();
            return;
        }

        var moveLeft = gameObject.transform.position.x - _tower.position.x > 0;

        if (moveLeft)
        {
            _rigidBody2D.velocity = new Vector2(-Speed, 0);
            gameObject.transform.localScale = new Vector3(3, 3, 1);
        }
        else
        {
            _rigidBody2D.velocity = new Vector2(Speed, 0);
            gameObject.transform.localScale = new Vector3(-3, 3, 1);
        }
    }

    private void Shot()
    {
        if (Time.time < _nextShot) return;

        var moveLeft = gameObject.transform.position.x - _tower.position.x > 0;
        var target = transform.FindChild("Target");
        var bullet = Instantiate(Resources.Load("EnemyBullet"), target.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().Shot(moveLeft ? -1 : 1, 5);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());

        _nextShot = Time.time + ShotTime;
    }
}
