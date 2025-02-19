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
        Debug.Log("�ε���Ұ���");
        StartCoroutine(LoadSceneAsync());
    }


    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("��ŸƮ�������� ����ʹ�.");
        MiniGameManager.Instance.StartGame(); // ���� ������ �ε�� �� StartGame ����
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