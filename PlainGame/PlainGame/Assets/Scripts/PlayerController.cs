using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    public float JumpForce = 10;
    public int MaxJump = 2;

    private Rigidbody2D _rigidBody2D;
    private int _jumpCount = 0;
    private Animator _animator;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Tower").GetComponent<Collider2D>());
    }

    void FixedUpdate()
    {
        var horizontalVelocity = Input.GetAxis("Horizontal");
        _rigidBody2D.velocity = new Vector2(horizontalVelocity * Speed, _rigidBody2D.velocity.y);
        _animator.SetFloat("Speed", Mathf.Abs(horizontalVelocity));

        if (horizontalVelocity < 0)
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        if (horizontalVelocity > 0)
            gameObject.transform.localScale = new Vector3(1, 1, 1);


        if (Input.GetButtonDown("Jump") && _jumpCount < MaxJump)
        {
            _rigidBody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            _jumpCount++;
            _animator.SetBool("Jump", true);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            var target = transform.FindChild("Target");
            var bullet = Instantiate(Resources.Load("Bullet"), target.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Bullet>().Shot((int)gameObject.transform.localScale.x);
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GameObject.FindGameObjectWithTag("Tower").GetComponent<Collider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _jumpCount = 0;
            _animator.SetBool("Jump", false);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            GameController.GetInstance().GameOver();
            DestroyObject(gameObject);
        }
    }
}
