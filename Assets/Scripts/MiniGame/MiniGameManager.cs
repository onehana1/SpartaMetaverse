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
    [SerializeField] private float elapsedTime = 0f;

    [SerializeField] private int miniGameScore = 0;
    [SerializeField] private int totalScore = 0;


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
                    Debug.Log("생성한다고요? 지금요?");
                    GameObject managerObject = new GameObject("MiniGameManager");
                    gameManager = managerObject.AddComponent<MiniGameManager>();
                }
            }
            return gameManager;
        }
    }




    private void Awake()
    {

        if (gameManager == null)
        {
            Debug.Log("설마 새로생기니?");
            gameManager = this;
        }

        uiManager = FindObjectOfType<MiniGameUIManager>();

        Time.timeScale = 0f;
    }

    private void Start()
    {
        Debug.Log("설마 새로생기니?");

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
            if (uiManager != null)
            {
                uiManager.gameObject.SetActive(true);
                uiManager.SetHomeGame();
            }
        }
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            MiniGameUIManager.Instance.gameUI.UpdateTimeUI(elapsedTime);
            SetTimeCnt(elapsedTime);
            CalTotalScore(miniGameScore, elapsedTime);
        }

    }

    public void SetTimeCnt(float time)
    {
        if (isGameOver) return;
        elapsedTime = time;
    }

    public void SetCoinCnt(int coin)
    {
        if (isGameOver) return;
        miniGameScore = coin;
        Debug.Log($"{miniGameScore}세팅 코인");
    }

    public void AddScore(int score)
    {
        Debug.Log($"{score}애드");
        miniGameScore += score;
        uiManager.gameUI.UpdateScoreUI(miniGameScore);
        SetCoinCnt(miniGameScore);
        Debug.Log($"{miniGameScore}코인");
    }

    public void CalTotalScore()
    {
        totalScore = 0; 
        totalScore += miniGameScore * 100;
        totalScore += (int)elapsedTime * 10;

        uiManager.gameOverUI.UpdateTotalScoreUI(totalScore);
    }
    public int CalTotalScore(int coin, float score)
    {
        int _score = 0;
        _score += coin * 100;
        _score += (int)score * 10;

        return _score;
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
        //CalTotalScore();
        Debug.Log($"elapsedTime{elapsedTime}");
        Debug.Log($"miniGameScore{miniGameScore}");

        float finalTime = elapsedTime;
        int finalScore = miniGameScore;

        totalScore = finalScore * 100 + (int)finalTime * 10;

        Debug.Log($"{totalScore} - 게임 오버!");

        uiManager.gameOverUI.UpdateTotalScoreUI(totalScore);

        GameManager.Instance.SetMiniGameScore(totalScore);

        if (uiManager == null)
        {
            Debug.LogError("MiniGameUIManager가 null");
            uiManager = FindObjectOfType<MiniGameUIManager>();
        }

        uiManager.gameUI.UpdateScoreUI(totalScore);
        uiManager.SetGameOver();
        isGameOver = true;
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
