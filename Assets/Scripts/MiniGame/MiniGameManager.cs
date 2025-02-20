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
                // ���� ������ MiniGameManager ã��
                gameManager = FindObjectOfType<MiniGameManager>();

                // ���� ������ ���� �����ϱ�
                if (gameManager == null)
                {
                    Debug.Log("�����Ѵٰ��? ���ݿ�?");
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
            Debug.Log("���� ���λ����?");
            gameManager = this;
        }

        uiManager = FindObjectOfType<MiniGameUIManager>();

        Time.timeScale = 0f;
    }

    private void Start()
    {
        Debug.Log("���� ���λ����?");

        StartCoroutine(InitializeMiniGameUI());
    }

    private IEnumerator InitializeMiniGameUI()
    {
        yield return null; // UI�� ������ ������ �ε�� ������ ��ٸ�
        uiManager = FindObjectOfType<MiniGameUIManager>();

        if (uiManager == null)
        {
            Debug.LogError("MiniGameUIManager ����.");
            yield break;
        }

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
        Debug.Log($"{miniGameScore}���� ����");
    }

    public void AddScore(int score)
    {
        Debug.Log($"{score}�ֵ�");
        miniGameScore += score;
        uiManager.gameUI.UpdateScoreUI(miniGameScore);
        SetCoinCnt(miniGameScore);
        Debug.Log($"{miniGameScore}����");
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
        Debug.Log("�̴ϰ��� ����!");
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

        Debug.Log($"{totalScore} - ���� ����!");

        uiManager.gameOverUI.UpdateTotalScoreUI(totalScore);

        GameManager.Instance.SetMiniGameScore(totalScore);

        if (uiManager == null)
        {
            Debug.LogError("MiniGameUIManager�� null");
            uiManager = FindObjectOfType<MiniGameUIManager>();
        }

        uiManager.gameUI.UpdateScoreUI(totalScore);
        uiManager.SetGameOver();
        isGameOver = true;
    }
    public void EndMiniGame()
    {
        // �̴ϰ��� ������ GameManager�� ����
        GameManager.Instance.SetMiniGameScore(totalScore);

        if (uiManager != null)
        {
            Destroy(uiManager.gameObject);
        }

        // ���������� �̵�
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");


    }

}
