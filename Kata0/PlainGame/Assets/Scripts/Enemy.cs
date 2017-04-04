using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 5;
    public Transform UpPoint;
    public Transform DownPoint;

    private bool _isUpMovement = false;

    void Start()
    {

    }

    void Update()
    {
        if (_isUpMovement)
        {
            transform.position = Vector2.MoveTowards(transform.position, UpPoint.position, Speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, DownPoint.position, Speed * Time.deltaTime);
        }

        if(Vector2.Distance(transform.position, UpPoint.position) < 1)
            _isUpMovement = false;
        if (Vector2.Distance(transform.position, DownPoint.position) < 1)
            _isUpMovement = true;
    }
}
