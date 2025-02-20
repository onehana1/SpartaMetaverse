using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameUIManager : UIManagerBase
{
    public HomeUI homeUI;
    public GameUI gameUI;
    public GameOverUI gameOverUI;
    private UIState currentState;

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

        ChangeState(UIState.Home);
    }


    public void SetHomeGame()
    {
        ChangeState(UIState.Home);
    }

    public void SetPlayGame()
    {
        ChangeState(UIState.Game);
    }

    public void SetGameOver()
    {
        Debug.Log("세팅 게임오버");
        ChangeState(UIState.GameOver);
    }


    public void ChangeState(UIState state)
    {
        currentState = state;
        homeUI.SetActive(currentState);
        gameUI.SetActive(currentState);
        gameOverUI.SetActive(currentState);
    }
}