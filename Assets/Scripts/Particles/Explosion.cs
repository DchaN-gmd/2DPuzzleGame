using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private float _lifeTime;

    private void Start()
    {
        _lifeTime = _particle.startLifetime;
    }

    private void OnEnable()
    {
        StartCoroutine(Activate());
    }

    private void OnDisable()
    {
        StopCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        _particle.Play();

        yield return new WaitForSeconds(_lifeTime);

        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
