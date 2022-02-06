using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _timeToShot;
    
    private float _timer;
    private Transform _barrel;

    private void Start()
    {
        _barrel = gameObject.GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if(_timer >= _timeToShot)
        {
            Instantiate(_bullet, _barrel.position, transform.rotation);
            _timer = 0;
        }
    }
}
