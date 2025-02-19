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
        ("���ٽ���", 2000),
        ("���ϸ�", 2500)
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
        bool playerExists = false;

        for (int i = 0; i < scoreData.Count; i++)
        {
            if (scoreData[i].name == "�÷��̾�")
            {
                scoreData[i] = ("�÷��̾�", playerScore); // ���� ���� ����
                playerExists = true;
                break;
            }
        }

        if (!playerExists)
        {
            scoreData.Add(("�÷��̾�", playerScore));
        }

        scoreData = scoreData.OrderByDescending(s => s.score).ToList();

        scoreText.text = "";
        foreach (var (name, score) in scoreData)
        {
            if (name == "�÷��̾�")
            {
                scoreText.text += $"<color=yellow>{name} - {score}��</color>\n";
            }
            else
            {
                scoreText.text += $"{name} - {score}��\n";
            }
        }
    }

    protected override UIState GetUIState()
    {
        return UIState.Score;
    }
}
