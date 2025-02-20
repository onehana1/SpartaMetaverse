using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUIManager : UIManagerBase
{
    public ScoreBoardUI scoreUI;

    private UIState currentState;
    public EnterUI enterUI;

    public static GameUIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        GameEventManager.OnEnterDoor += HandleEnterDoor;
        GameEventManager.OnEnterCraneDoor += HandleEnterCraneDoor;
        GameEventManager.OnEnterTomstone += HandleEnterTomstone;

    }

    private void OnDisable()
    {
        GameEventManager.OnEnterDoor -= HandleEnterDoor;
        GameEventManager.OnEnterCraneDoor -= HandleEnterCraneDoor;
        GameEventManager.OnEnterTomstone -= HandleEnterTomstone;

    }

    private void HandleEnterDoor(bool state)
    {
        if (state)
        {
            enterUI.gameObject.SetActive(true);
            enterUI.UpdateGameInfo(0);
        }
    }

    private void HandleEnterCraneDoor(bool state)
    {
        if (state)
        {
            enterUI.gameObject.SetActive(true);
            enterUI.UpdateGameInfo(1);
        }
    }

    private void HandleEnterTomstone(bool state)
    {
        if (state)
        {
            int highScore = ScoreManager.Instance.GetMiniGameScore();
            scoreUI.UpdateScoreUI(highScore);
            OpenScoreBoard();
        }
    }


    public void OpenScoreBoard()
    {
        scoreUI.gameObject.SetActive(true);
    }

    public void CloseScoreBoard()
    {
        scoreUI.gameObject.SetActive(false);
    }


    public void CloseEnterUI()
    {
        enterUI.gameObject.SetActive(false);
    }



}
