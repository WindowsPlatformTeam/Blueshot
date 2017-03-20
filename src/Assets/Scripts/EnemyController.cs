using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Patrol,
    Alert,
    Follow,
    Shot
}

public enum PatrolType
{
    Loop,
    PingPong
}

public class EnemyController : MonoBehaviour
{
    public PatrolType PatrolType;
    public float Speed;
    public float RotationSpeed;
    public List<GameObject> PatrolPoints;
    public float StopDistance;

    private EnemyState _state = EnemyState.Idle;
    private GameObject _nextPatrolPoint;
    private GameObject _currentPoint;
    private bool _isFirstDirection;
    private GameObject _player;
    private float _timeToFire = 0;

    private void Start()
    {
        _currentPoint = PatrolPoints[0];
        _nextPatrolPoint = PatrolPoints[1];
        _isFirstDirection = true;
    }

    void Update ()
    {
        UpdateEnemyState();
	}

    private void UpdateEnemyState()
    {
        switch (_state)
        {
            case EnemyState.Idle:
                SetState(EnemyState.Patrol);
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Alert:
                SetState(EnemyState.Follow);
                break;
            case EnemyState.Follow:
                Follow();
                break;
            case EnemyState.Shot:
                Shot();
                break;
            default:
                break;
        }
    }

    private void SetState(EnemyState state)
    {
        _state = state;
    }

    private void Patrol()
    {
        if (PatrolPoints == null) return;

        var distance = Vector3.Distance(transform.position, _currentPoint.transform.position);
        if (distance < 0.5f)
        {
            SetNextPatrolPoint();
        }

        RotateToTarget(_nextPatrolPoint.transform);
        transform.position = Vector2.MoveTowards(transform.position, _nextPatrolPoint.transform.position, Speed * Time.deltaTime);
    }

    private void SetNextPatrolPoint()
    {
        switch (PatrolType)
        {
            case PatrolType.Loop:
                LoopPatrol();
                break;
            case PatrolType.PingPong:
                PingPongPatrol();
                break;
            default:
                break;
        }
        
        _currentPoint = _nextPatrolPoint;
    }

    private void LoopPatrol()
    {
        var index = PatrolPoints.FindIndex(point => point == _currentPoint);

        if (index < PatrolPoints.Count - 1)
        {
            _nextPatrolPoint = GetNextPatrolPoint(index);
        }
        else
        {
            _nextPatrolPoint = GetStartPatrolPoint();
        }
    }

    private void PingPongPatrol()
    {
        var index = PatrolPoints.FindIndex(point => point == _currentPoint);

        if (_isFirstDirection)
        {
            if (index < PatrolPoints.Count - 1)
            {
                _nextPatrolPoint = GetNextPatrolPoint(index);
            }
            else
            {
                _nextPatrolPoint = GetPreviousPatrolPoint(index);
                _isFirstDirection = false;
            }
        }
        else
        {
            if (index > 0)
            {
                _nextPatrolPoint = GetPreviousPatrolPoint(index);
            }
            else
            {
                _nextPatrolPoint = GetNextPatrolPoint(index);
                _isFirstDirection = true;
            }
        }
    }

    private void Follow()
    {
        if (_player == null) return;

        RotateToTarget(_player.transform);

        var distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance > StopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, Speed * Time.deltaTime);
        }

        Shot();
    }

    private void Shot()
    {
        _timeToFire -= Time.deltaTime;
        if (_timeToFire <= 0)
        {
            Fire();
            _timeToFire = 1f;
        }
    }

    private void Fire()
    {
        Instantiate(Resources.Load("Bullet"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }

    private GameObject GetNextPatrolPoint(int index)
    {
       return PatrolPoints[index + 1];
    }

    private GameObject GetPreviousPatrolPoint(int index)
    {
        return PatrolPoints[index - 1];
    }

    private GameObject GetStartPatrolPoint()
    {
        return PatrolPoints[0];
    }

    private void RotateToTarget(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            _player = coll.gameObject;
            SetState(EnemyState.Alert);
        }

    }
}
