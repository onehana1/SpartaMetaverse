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
        Debug.Log("�ε���Ұ���");
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
        Debug.Log("��ŸƮ�������� ����ʹ�.");
        MiniGameManager.Instance.StartGame();
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
    }



    public void OnClickExitButton()
    {
        MiniGameManager.Instance.EndMiniGame();
       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateTotalScoreUI(int score)
    {
        totalText.text = $"{score}��";

    }

    protected override UIState GetUIState()
    {
        return UIState.GameOver;
    }
}