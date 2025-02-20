using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterUI : BaseUI
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button enterButton;
    [SerializeField] private TextMeshProUGUI gameNameText;
    [SerializeField] private TextMeshProUGUI gameDisText;

    private List<(string name, string description)> miniGameData = new List<(string, string)>()    
    {
        ("�������� ����!", "���콺 ��ư�� ������ �� �� �ִ�! \r\n������ ������ ������ �� �� �ö��"),
        ("���۹�� �����̱�", "Ű���� ����Ű�� ��������\r\n�Ʒ�Ű�� ��� ����!\r\nVŰ�� ������ ���� ���� �� �־��")
    };


    private GameManager gameManager;
    public PlayerController playerController;


    void Start()
    {
        gameManager = GameManager.Instance;
        playerController = FindObjectOfType<PlayerController>();

        exitButton.onClick.AddListener(OnClickExitButton);
        enterButton.onClick.AddListener(OnClickEnterButton);
    }

    public void OnClickExitButton()
    {
        GameUIManager.Instance.CloseEnterUI();
    }

    public void OnClickEnterButton()
    {
        if (playerController != null && playerController.isNearDoor)
        {
            Debug.Log("2�ǳ�");
            playerController.isNearDoor = false;
            SceneManager.LoadScene("MiniGameScene_Flappy_Bird");         
            GameUIManager.Instance.CloseEnterUI();

        }
        if (playerController != null && playerController.isNearCraneDoor)
        {
            Debug.Log("3�ǳ�");
            playerController.isNearCraneDoor = false;
            SceneManager.LoadScene("MiniGameScene");     
            GameUIManager.Instance.CloseEnterUI();

        }

    }
    public void UpdateGameInfo()
    {
        if (playerController != null)
        {
            if (playerController.isNearDoor)
            {
                gameNameText.text = miniGameData[0].name;
                gameDisText.text = miniGameData[0].description;
            }
            if (playerController.isNearCraneDoor)
            {
                gameNameText.text = miniGameData[1].name;
                gameDisText.text = miniGameData[1].description;
            }
        }
    }


    protected override UIState GetUIState()
    {
        return UIState.Enter;
    }

}
