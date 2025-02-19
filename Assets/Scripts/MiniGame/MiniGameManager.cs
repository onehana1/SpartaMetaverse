using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MiniGameManager : MonoBehaviour
{
    static MiniGameManager gameManager;

    private UIManager uiManager;
    public static bool isFirstLoading = true;
    public static MiniGameManager Instance
    {
        get { return gameManager; }
    }

    private int miniGameScore = 0;
    private bool isGameStarted = false;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
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
        if (!isFirstLoading)
        {
            StartGame();
        }
        else
        {
            isFirstLoading = false;
        }
    }



    public void AddScore(int score)
    {
        miniGameScore += score;
    }

    public void StartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1f;
        uiManager.SetPlayGame();
        Debug.Log("미니게임 시작!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        Debug.Log("Game Over");
    }
    public void EndMiniGame()
    {
        // 미니게임 점수를 GameManager에 저장
        GameManager.Instance.SetMiniGameScore(miniGameScore);

        // 본게임으로 이동
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");


    }
}
