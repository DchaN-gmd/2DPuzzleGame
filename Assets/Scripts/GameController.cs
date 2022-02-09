using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<FinishPoint> _finishPoints;

    private int _charactersCount;
    private int _charactersFinished;
    private Abyss _abyss;
    private ChangeCharacter _changeCharacter;

    [HideInInspector] public static UnityAction _playerDied;

    private void Start()
    {
        _charactersCount = FindObjectOfType<ChangeCharacter>().GetComponent<ChangeCharacter>().CharactersCount;
        _finishPoints.AddRange(gameObject.GetComponentsInChildren<FinishPoint>());

        SubscribeToGameOverEvent();
    }

    private void SubscribeToGameOverEvent()
    {
        _abyss = FindObjectOfType<Abyss>().GetComponent<Abyss>();
        _abyss.playerFall.AddListener(GameOver);

        Bullet.playerShooted += GameOver;

        foreach (FinishPoint point in _finishPoints)
        {
            point.characterComedOnFinish.AddListener(OnFinishPoint);
            point.characterGoedOutFinish.AddListener(OnOutFinishPoint);
        }
    }

    private void OnDisable()
    {
        foreach (FinishPoint point in _finishPoints)
        {
            point.characterComedOnFinish.RemoveListener(OnFinishPoint);
            point.characterGoedOutFinish.RemoveListener(OnOutFinishPoint);
        }
        _abyss.playerFall.RemoveListener(GameOver);
        Bullet.playerShooted -= GameOver;
    }

    private void OnFinishPoint()
    {
        _charactersFinished++;
        if (_charactersFinished == _charactersCount)
        {
            FinishGame();
        }
    }

    private void OnOutFinishPoint() => _charactersFinished--;

    private void FinishGame()
    {
        Debug.Log("ХААААААААААААААРРРОШЬ");
    }

    private void GameOver()
    {
        _playerDied?.Invoke();
        Time.timeScale = 0.15f;
        Debug.Log("Game over");
    }
}
