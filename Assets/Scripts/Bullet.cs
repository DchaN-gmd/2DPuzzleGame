using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("BulletSpeed"), SerializeField]
    private float _speed;

    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();    
    }

    
    void FixedUpdate()
    {
        _rigidbody.velocity = Vector2.right * _speed * Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Character character))
        {
            Debug.Log("Shoot");
            
        }
        Destroy(gameObject);
    }
}
