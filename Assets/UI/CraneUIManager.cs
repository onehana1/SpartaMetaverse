using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CraneUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // 점수 표시할 UI
    [SerializeField] private Button exitButton; // Exit 버튼
    [SerializeField] private Button retryButton; // retry



    private int curGiftCnt = 0; // 현재 잡은 선물 개수
    private int maxGiftCnt = 6; // 최대 잡을 수 있는 개수

    private void Start()
    {
        if (exitButton == null)
        {
            exitButton = FindObjectOfType<Button>(); // 버튼 자동 찾기
        }
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClicked);
        }
        else
        {
            Debug.LogError("Exit Button");
        }

        UpdateScoreUI();
    }


    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene("MiniGameScene");
    }


    public void UpdateScoreUI()
    {
        scoreText.text = $"{curGiftCnt}/{maxGiftCnt}개 잡음!";
    }

    public void IncreaseGiftCount()
    {
        if (curGiftCnt < maxGiftCnt)
        {
            curGiftCnt++;
            UpdateScoreUI();
        }
    }

    public void DecreaseGiftCount()
    {
        if (curGiftCnt > 0)
        {
            curGiftCnt--;
            UpdateScoreUI();
        }
    }
}
