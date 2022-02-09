using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _timeToShot;
    [SerializeField] private Transform _barrel;

    private float _timer;

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        _timer += Time.deltaTime;

        if (_timer >= _timeToShot)
        {
            var bullet = Instantiate(_bullet, _barrel.position, _barrel.transform.rotation);
            _timer = 0;
        }
    }
}
