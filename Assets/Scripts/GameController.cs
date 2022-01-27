using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int _charactersCount;
    [SerializeField]private int _charactersFinished;
    private ChangeCharacter _changeCharacter;
    private List<FinishPoint> _finishPoints;

    private void Start()
    {
        ChangeCharacter _changeCharacter = new ChangeCharacter();
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
