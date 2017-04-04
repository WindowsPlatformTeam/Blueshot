using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 50;

	void Start ()
    {
	}

    private void OnBecameInvisible()
    {
        DestroyObject(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestroyObject(gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            DestroyObject(collision.gameObject);
            GameController.GetInstance().AddScore(1);
        }
    }

    public void Shot(int direction)
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector2(Speed * direction, 0);
    }
}
