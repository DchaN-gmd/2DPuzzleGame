using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameController._playerDied += OnGameOver;
    }

    private void OnDisable()
    {
        GameController._playerDied -= OnGameOver;
    }

    private void OnGameOver()
    {
        _animator.Play("GameOverPanel");
    }
}
