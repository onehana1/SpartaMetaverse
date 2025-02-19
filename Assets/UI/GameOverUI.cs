using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManager uiManager)
    {
        base.Init(uiManager);
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        Debug.Log("로드씬할거임");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("스타트게임으로 가고싶다.");
        MiniGameManager.Instance.StartGame();
    }

    public void OnClickExitButton()
    {
        MiniGameManager.Instance.EndMiniGame();
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}