using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : BaseUI
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TextMeshProUGUI totalText;

    public override void Init(UIManagerBase uiManager)
    {
        base.Init(uiManager);
        restartButton.onClick.AddListener(OnClickRestartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickRestartButton()
    {
        Debug.Log("로드씬할거임");
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(LoadSceneAsync());

    }

    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("스타트게임으로 가고싶다.");
        MiniGameManager.Instance.StartGame();
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }



    public void OnClickExitButton()
    {
        MiniGameManager.Instance.EndMiniGame();
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateTotalScoreUI(int score)
    {
        totalText.text = $"{score}점";

    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}