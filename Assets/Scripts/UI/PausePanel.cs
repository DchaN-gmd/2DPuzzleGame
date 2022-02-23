using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    public void ShowPausePanel()
    {
        _animator.Play("ShowPausePanel");
        Time.timeScale = 0;
    }

    public void HidePausePanel()
    {
        _animator.Play("HidePausePanel");
        Time.timeScale = 1;
    }
}
