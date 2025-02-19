using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button exitButton;

    private GameManager gameManager;

    private List<(string name, int score)> scoreData = new List<(string, int)>()    // data
    {
        ("세바스찬", 2000),
        ("헤일리", 2500)
    };

    void Start()
    {
        gameManager = GameManager.Instance;
        exitButton.onClick.AddListener(OnClickExitButton);
    }

    public void OnClickExitButton()
    {
        GameUIManager.Instance.CloseScoreBoard();
    }


    public void UpdateScoreUI(int playerScore)
    {
        scoreData.Add(("플레이어", playerScore));

        scoreData = scoreData.OrderByDescending(s => s.score).ToList();

        scoreText.text = "";
        foreach (var (name, score) in scoreData)
        {
            if (name == "플레이어")
            {
                scoreText.text += $"<color=yellow>{name} - {score}점</color>\n";
            }
            else
            {
                scoreText.text += $"{name} - {score}점\n";
            }
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }
}
