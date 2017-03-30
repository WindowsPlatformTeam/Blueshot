using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float Speed = 100f;

    private Rigidbody2D _rigidbody;
    private Vector2 _initialPlayerPosition;
    private GameController _gameController;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _initialPlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        _gameController = GameController.GetInstance();
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

        if(coll != null && coll.gameObject.tag == "Player")
        {
            _gameController.GameOver();
            Destroy(coll.gameObject);
        }
    }
}
