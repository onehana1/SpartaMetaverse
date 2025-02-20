using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameUIManager : UIManagerBase
{
    public ScoreBoardUI scoreUI;

    private UIState currentState;
    public EnterUI enterUI; // Enter UI ÂüÁ¶

    public static GameUIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
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

    public void OpenEnterUI()
    {
        enterUI.gameObject.SetActive(true);
        enterUI.UpdateGameInfo();
    }

    public void CloseEnterUI()
    {
        enterUI.gameObject.SetActive(false);
    }



}
