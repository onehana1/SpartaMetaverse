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

    private int curGiftCnt = 0; // ���� ���� ���� ����
    private int maxGiftCnt = 5; // �ִ� ���� �� �ִ� ����

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
        ScoreManager.Instance.SetCaughtGiftCount(curGiftCnt);
        SceneManager.LoadScene("SampleScene");
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
