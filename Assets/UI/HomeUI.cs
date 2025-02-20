using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    public override void Init(UIManagerBase uiManager)
    {
        base.Init(uiManager);
        startButton.onClick.AddListener(OnClickStartButton);
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickStartButton()
    {
        MiniGameManager.Instance.StartGame();
    }

    public void OnClickExitButton()
    {
        MiniGameManager.Instance.EndMiniGame();
    }

    protected override UIState GetUIState()
    {
        return UIState.Home;
    }
}