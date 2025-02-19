using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MiniGameUIState
{
    Home,
    Game,
    GameOver,
}

public class MiniGameUIManager : MonoBehaviour
{
    public HomeUI homeUI;
    public GameUI gameUI;
    public GameOverUI gameOverUI;
    private MiniGameUIState currentState;

    public static MiniGameUIManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        homeUI = GetComponentInChildren<HomeUI>(true);
        homeUI.Init(this);
        gameUI = GetComponentInChildren<GameUI>(true);
        gameUI.Init(this);
        gameOverUI = GetComponentInChildren<GameOverUI>(true);
        gameOverUI.Init(this);

        ChangeState(MiniGameUIState.Home);
    }

    public void SetPlayGame()
    {
        ChangeState(MiniGameUIState.Game);
    }

    public void SetGameOver()
    {
        ChangeState(MiniGameUIState.GameOver);
    }


    public void ChangeState(MiniGameUIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
}