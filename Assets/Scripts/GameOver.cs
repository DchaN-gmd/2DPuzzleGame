using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : MonoBehaviour
{
    public UnityEvent<Character> _playerDead;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Character character))
        {
            _playerDead?.Invoke(collision.gameObject.GetComponent<Character>());
            Debug.Log("Loser");
        }
    }
}
