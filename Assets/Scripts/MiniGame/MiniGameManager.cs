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
            Debug.Log("처음 아님!");
        }
        else
        {
            isFirstLoading = false;
            Debug.Log("처음임!");
        }
    }



    public void AddScore(int score)
    {
        miniGameScore += score;
    }

    public void StartGame()
    {
        uiManager.SetPlayGame();
        Debug.Log("미니게임 시작!");
        StartCoroutine(DelayedTimeScaleChange());
    }
    private IEnumerator DelayedTimeScaleChange()
    {
        yield return null;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        // Debug.Log("Game Over");
        uiManager.SetGameOver();
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
