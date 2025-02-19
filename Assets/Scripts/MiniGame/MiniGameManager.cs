using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MiniGameManager : MonoBehaviour
{
    static MiniGameManager gameManager;

    private UIManager uiManager;
    public bool isFirstLoading = true;
    public bool isGameOver = false;
    private float elapsedTime = 0f;
    public static MiniGameManager Instance
    {
        get { return gameManager; }
    }

    private int miniGameScore = 0;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        uiManager = FindObjectOfType<UIManager>();

        Time.timeScale = 0f;
    }

    private void Start()
    {
        uiManager = UIManager.Instance;
        if (!isFirstLoading)
        {
            StartGame();
            Debug.Log("ó�� �ƴ�!");
        }
        else
        {
            isFirstLoading = false;
            Debug.Log("ó����!");
        }
    }

    private void Update()
    {
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            UIManager.Instance.gameUI.UpdateTimeUI(elapsedTime);
        }
    }

    public void AddScore(int score)
    {
        miniGameScore += score;
        uiManager.gameUI.UpdateScoreUI(miniGameScore);
    }

    public void CalTotalScore()
    {
        int totalScore = 0;
        totalScore += miniGameScore * 100;
        totalScore += (int)elapsedTime * 10;

        uiManager.gameOverUI.UpdateTotalScoreUI(totalScore);

    }

    public void StartGame()
    {
        isGameOver = false;
        elapsedTime = 0f;
        uiManager.SetPlayGame();
        Debug.Log("�̴ϰ��� ����!");
        StartCoroutine(DelayedTimeScaleChange());
    }
    private IEnumerator DelayedTimeScaleChange()
    {
        yield return null;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (isGameOver) return;
        CalTotalScore();
        isGameOver = true;
        uiManager.SetGameOver();
    }
    public void EndMiniGame()
    {
        // �̴ϰ��� ������ GameManager�� ����
        GameManager.Instance.SetMiniGameScore(miniGameScore);

        // ���������� �̵�
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");


    }
}
