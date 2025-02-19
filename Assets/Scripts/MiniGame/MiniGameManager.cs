using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MiniGameManager : MonoBehaviour
{
    static MiniGameManager gameManager;

    public static MiniGameManager Instance
    {
        get { return gameManager; }
    }

    private int miniGameScore = 0; // 미니게임 점수
    private void Awake()
    {
        gameManager = this;
    }
    public void AddScore(int score)
    {
        miniGameScore += score;
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
        SceneManager.LoadScene("SampleScene");

    }
}
