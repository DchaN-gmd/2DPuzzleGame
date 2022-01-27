using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private CharacterColor _targetCharacter;

    [HideInInspector] public UnityEvent _characterComedOnFinish;
    [HideInInspector] public UnityEvent _characterGoedOutFinish;

    private enum CharacterColor
    {
        pink,
        blue
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PinkCharacter pinkCharacter) && _targetCharacter == CharacterColor.pink)
        {
            _characterComedOnFinish?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out BlueCharacter blueCharacter) && _targetCharacter == CharacterColor.blue)
        {
            _characterComedOnFinish?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PinkCharacter pinkCharacter) && _targetCharacter == CharacterColor.pink)
        {
            _characterGoedOutFinish?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out BlueCharacter blueCharacter) && _targetCharacter == CharacterColor.blue)
        {
            _characterGoedOutFinish?.Invoke();
        }
    }
}
