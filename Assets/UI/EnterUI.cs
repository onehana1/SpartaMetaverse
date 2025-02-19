using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterUI : BaseUI
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button enterButton;


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


    protected override UIState GetUIState()
    {
        return UIState.Enter;
    }

}
