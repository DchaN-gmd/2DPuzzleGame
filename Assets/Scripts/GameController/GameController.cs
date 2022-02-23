using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;
public class GameController : MonoBehaviour
{
    [SerializeField] private List<FinishPoint> _finishPoints;

    private int _charactersCount;
    private int _charactersFinished;
    private Abyss _abyss;
    private ChangeCharacter _changeCharacter;

    [SerializeField] private List<IPlayerKillable> _playerKillables = new List<IPlayerKillable>();

    [HideInInspector] public static UnityAction _playerDied;

    private void Start()
    {
        _charactersCount = FindObjectOfType<ChangeCharacter>().GetComponent<ChangeCharacter>().CharactersCount;
        _finishPoints.AddRange(FindObjectsOfType<FinishPoint>());

        _playerKillables.AddRange(GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IPlayerKillable>());

        SubscribeToGameOverEvent();
    }

    private void SubscribeToGameOverEvent()
    {
        Bullet.playerShooted.AddListener(GameOver);

        for (int i = 0; i < _playerKillables.Count; i++)
        {
            _playerKillables[i].OnPlayerDied.AddListener(GameOver);
        }

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

        foreach (var deathPoint in _playerKillables)
        {
            deathPoint.OnPlayerDied.RemoveListener(GameOver);
        }

        Bullet.playerShooted.RemoveListener(GameOver);
    }

    private void OnFinishPoint()
    {
        _charactersFinished++;

        if (_charactersFinished == _charactersCount)
            FinishGame();
    }

    private void OnOutFinishPoint() => _charactersFinished--;

    private void FinishGame()
    {
        Application.Quit();
    }

    private void GameOver()
    {
        _playerDied?.Invoke();
        Time.timeScale = 0.15f;
        Debug.Log("Game over");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
