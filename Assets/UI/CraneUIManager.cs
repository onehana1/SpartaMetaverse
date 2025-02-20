using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CraneUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText; // ���� ǥ���� UI
    [SerializeField] private Button exitButton; // Exit ��ư
    [SerializeField] private Button retryButton; // retry



    private int curGiftCnt = 0; // ���� ���� ���� ����
    private int maxGiftCnt = 6; // �ִ� ���� �� �ִ� ����

    private void Start()
    {
        if (exitButton == null)
        {
            exitButton = FindObjectOfType<Button>(); // ��ư �ڵ� ã��
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
        scoreText.text = $"{curGiftCnt}/{maxGiftCnt}�� ����!";
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
