using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrolEnemy : MonoBehaviour, IPlayerKillable
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _patrolPoints;

    private int _currentPoint;

    public UnityEvent OnPlayerDied { get; } = new UnityEvent();

    private void Update()
    {
        Patrol();
    }

    private void Patrol()
    { 
        transform.position = Vector3.MoveTowards(transform.position, _patrolPoints[_currentPoint].position, _speed * Time.deltaTime);

        if (transform.position == _patrolPoints[_currentPoint].position)
        {
            _currentPoint++;
        
            if (_currentPoint == _patrolPoints.Length)
                        _currentPoint = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Character character))
        {
            Destroy(collision.gameObject);
            OnPlayerDied?.Invoke();
        }

        Destroy(gameObject);
    }
}
