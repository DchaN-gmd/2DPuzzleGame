using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private List<FinishPoint> _finishPoints;

    private int _charactersCount;
    private int _charactersFinished;
    private VictoryPanel _victoryPanel;

    [SerializeField] private List<IPlayerKillable> _playerKillables = new List<IPlayerKillable>();

    [HideInInspector] public static UnityAction _playerDied;

    private void Start()
    {
        _charactersCount = FindObjectOfType<ChangeCharacter>().GetComponent<ChangeCharacter>().CharactersCount;
        _finishPoints.AddRange(FindObjectsOfType<FinishPoint>());
        _victoryPanel = FindObjectOfType<VictoryPanel>().GetComponent<VictoryPanel>();

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
        Time.timeScale = 0.15f;
        _victoryPanel.ShowVictoryPanel();
    }

    private void GameOver()
    {
        _playerDied?.Invoke();
        Time.timeScale = 0.15f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
