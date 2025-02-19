using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;

    private MiniGameManager gameManager;
    private void Start()
    {
        gameManager = MiniGameManager.Instance;
        UpdateScoreUI(0); // √ ±‚»≠
        UpdateTimeUI(0f);
    }
    public void UpdateScoreUI(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateTimeUI(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = $"{minutes}:{seconds}";
    }

    protected override MiniGameUIState GetUIState()
    {
        return MiniGameUIState.Game;
    }
}