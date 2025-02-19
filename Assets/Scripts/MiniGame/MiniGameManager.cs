using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MiniGameManager : MonoBehaviour
{
    static MiniGameManager gameManager;

    private MiniGameUIManager uiManager;
    public bool isFirstLoading = true;
    public bool isGameOver = false;

    public bool isStart = true;
    private float elapsedTime = 0f;
    public static MiniGameManager Instance
    {
        get
        {
            if (gameManager == null)
            {
                // 현재 씬에서 MiniGameManager 찾기
                gameManager = FindObjectOfType<MiniGameManager>();

                // 씬에 없으면 새로 생성하기
                if (gameManager == null)
                {
                    GameObject managerObject = new GameObject("MiniGameManager");
                    gameManager = managerObject.AddComponent<MiniGameManager>();
                }
            }
            return gameManager;
        }
    }


    private int miniGameScore = 0;
    private int totalScore = 0;


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

        uiManager = FindObjectOfType<MiniGameUIManager>();

        Time.timeScale = 0f;
    }

    private void Start()
    {
        StartCoroutine(InitializeMiniGameUI());
    }

    private IEnumerator InitializeMiniGameUI()
    {
        yield return null; // UI가 씬에서 완전히 로드될 때까지 기다림
        uiManager = FindObjectOfType<MiniGameUIManager>();

        if (uiManager == null)
        {
            Debug.LogError("MiniGameUIManager 에러.");
            yield break;
        }

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

    private void Update()
    {
        if(isStart)
        {
            uiManager.gameObject.SetActive(true);
            uiManager.SetHomeGame();
        }
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            MiniGameUIManager.Instance.gameUI.UpdateTimeUI(elapsedTime);
        }
    }


    public void AddScore(int score)
    {
        miniGameScore += score;
        uiManager.gameUI.UpdateScoreUI(miniGameScore);
    }

    public int CalTotalScore()
    {
        int totalScore = 0;
        totalScore += miniGameScore * 100;
        totalScore += (int)elapsedTime * 10;

        uiManager.gameOverUI.UpdateTotalScoreUI(totalScore);
        return totalScore;
    }

    public void StartGame()
    {
        isGameOver = false;
        elapsedTime = 0f;
        uiManager.SetPlayGame();
        Debug.Log("미니게임 시작!");
        StartCoroutine(DelayedTimeScaleChange());
        isStart = false;
    }
    private IEnumerator DelayedTimeScaleChange()
    {
        yield return null;
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
       // if (isGameOver) return;
        totalScore = CalTotalScore();
        isGameOver = true;
        Debug.Log("게임 오버!");


        GameManager.Instance.SetMiniGameScore(totalScore);

        if (uiManager == null)
        {
            Debug.LogError("MiniGameUIManager가 null입니다! 다시 찾습니다.");
            uiManager = FindObjectOfType<MiniGameUIManager>();
        }

        uiManager.gameUI.UpdateScoreUI(totalScore);

        uiManager.SetGameOver();
    }
    public void EndMiniGame()
    {
        // 미니게임 점수를 GameManager에 저장
        GameManager.Instance.SetMiniGameScore(totalScore);

        if (uiManager != null)
        {
            Destroy(uiManager.gameObject);
        }



        // 본게임으로 이동
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");


    }

}
