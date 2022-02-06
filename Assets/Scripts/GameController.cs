using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private ChangeCharacter _changeCharacter;
    [SerializeField] private List<FinishPoint> _finishPoints;

    private int _charactersCount;
    private int _charactersFinished;
    
    private void Start()
    {
        _charactersCount = _changeCharacter.CharactersCount;
        _finishPoints.AddRange(gameObject.GetComponentsInChildren<FinishPoint>());

        foreach(FinishPoint point in _finishPoints)
        {
            point._characterComedOnFinish.AddListener(OnFinishPoint);
            point._characterGoedOutFinish.AddListener(OnOutFinishPoint);
        }
    }

    private void OnDisable()
    {
        foreach (FinishPoint point in _finishPoints)
        {
            point._characterComedOnFinish.RemoveListener(OnFinishPoint);
            point._characterGoedOutFinish.RemoveListener(OnOutFinishPoint);
        }
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
}
