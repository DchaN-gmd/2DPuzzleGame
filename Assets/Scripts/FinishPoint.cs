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
        Pink,
        Blue
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PinkCharacter pinkCharacter) && _targetCharacter == CharacterColor.Pink)
        {
            _characterComedOnFinish?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out BlueCharacter blueCharacter) && _targetCharacter == CharacterColor.Blue)
        {
            _characterComedOnFinish?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PinkCharacter pinkCharacter) && _targetCharacter == CharacterColor.Pink)
        {
            _characterGoedOutFinish?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out BlueCharacter blueCharacter) && _targetCharacter == CharacterColor.Blue)
        {
            _characterGoedOutFinish?.Invoke();
        }
    }
}
