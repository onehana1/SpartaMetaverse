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
                // ���� ������ MiniGameManager ã��
                gameManager = FindObjectOfType<MiniGameManager>();

                // ���� ������ ���� �����ϱ�
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
       // if (isGameOver) return;
        totalScore = CalTotalScore();
        isGameOver = true;
        Debug.Log("���� ����!");


        GameManager.Instance.SetMiniGameScore(totalScore);

        if (uiManager == null)
        {
            Debug.LogError("MiniGameUIManager�� null�Դϴ�! �ٽ� ã���ϴ�.");
            uiManager = FindObjectOfType<MiniGameUIManager>();
        }

        uiManager.gameUI.UpdateScoreUI(totalScore);

        uiManager.SetGameOver();
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
