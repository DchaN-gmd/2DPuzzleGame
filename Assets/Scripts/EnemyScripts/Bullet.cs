using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [Header("BulletSpeed"), SerializeField]
    private float _speed;
    [SerializeField] 
    float _timeToDestroy;

    private Rigidbody2D _rigidbody;

    public static UnityAction playerShooted;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        Destroy(gameObject, _timeToDestroy); 
    }

    void Update()
    {
        _rigidbody.velocity = transform.right * _speed * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out ShooterEnemy shooter))
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out Character character))
        {
            Destroy(collision.gameObject);
            playerShooted?.Invoke();
        }

        Destroy(gameObject);
    }
}
