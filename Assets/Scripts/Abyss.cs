using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Abyss : MonoBehaviour
{
    public UnityEvent playerFall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Character character))
        {
            playerFall?.Invoke();
            Debug.Log("Loser");
        }
    }
}
