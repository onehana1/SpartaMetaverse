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
        ("오래오래 날기!", "마우스 버튼을 누르면 날 수 있다! \r\n코인을 먹으면 점수가 더 잘 올라요"),
        ("빙글뱅글 인형뽑기", "키보드 방향키로 움직여요\r\n아래키로 잡기 도전!\r\nV키를 누르면 내려 놓을 수 있어요")
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
            Debug.Log("2되나");
            playerController.isNearDoor = false;
            SceneManager.LoadScene("MiniGameScene_Flappy_Bird");         
            GameUIManager.Instance.CloseEnterUI();

        }
        if (playerController != null && playerController.isNearCraneDoor)
        {
            Debug.Log("3되나");
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
