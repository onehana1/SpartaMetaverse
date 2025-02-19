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

    public override void Init(MiniGameUIManager uiManager)
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

    public void UpdateTotalScoreUI(int score)
    {
        totalText.text = $"{score}��";

    }

    protected override MiniGameUIState GetUIState()
    {
        return MiniGameUIState.GameOver;
    }
}