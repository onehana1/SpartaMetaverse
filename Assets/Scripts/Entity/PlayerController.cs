using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    private Camera camera;
    private bool isNearDoor = false;
    private bool isNearTomstone = false;
    private bool isNearCraneDoor = false;
    [SerializeField] private GameObject playerImage;
    [SerializeField] private GameObject RidePivot;
    private bool isRide = false;



    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


        // �̵� ���� ���� �ٶ󺸴� ������ ������Ʈ

        if (isShift)
        {
            movementDirection = Vector2.zero;

            // movementDirection �갡 0�̸� ���� ������ �׷��� ���ο� ���� ���� ���� ���ϱ�
            Vector2 newLookDirection = new Vector2(horizontal, vertical).normalized;           
            if (newLookDirection != Vector2.zero)
            {
                lookDirection = newLookDirection;
            }
        }
        else
        {
            movementDirection = new Vector2(horizontal, vertical).normalized;
            if (movementDirection != Vector2.zero)
            {
                lookDirection = movementDirection;
            }
        }

        if (isNearDoor && Input.GetKeyDown(KeyCode.F))
        {
            isNearDoor = false;
            SceneManager.LoadScene("MiniGameScene_Flappy_Bird");
        }
        if (isNearTomstone && Input.GetKeyDown(KeyCode.F))
        {
            isNearTomstone = false;
            int highScore = GameManager.Instance != null ? GameManager.Instance.GetMiniGameScore() : 0;
            GameUIManager.Instance.scoreUI.UpdateScoreUI(highScore);
            GameUIManager.Instance.OpenScoreBoard();

        }
        if (isNearCraneDoor && Input.GetKeyDown(KeyCode.F))
        {
            isNearCraneDoor = false;
            SceneManager.LoadScene("MiniGameScene");
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isRide)
            {
                playerImage.SetActive(false);
                RidePivot.SetActive(true);
            }
            else
            {
                playerImage.SetActive(true);
                RidePivot.SetActive(false);
            }
            isRide = !isRide;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HouseDoor"))
        {
            isNearDoor = true;
            Debug.Log("�� ��ó�� ����");
        }
        if (other.CompareTag("Tombstone")) 
        {
            isNearTomstone = true;
            Debug.Log("�� ��ó�� ����");
        }
        if (other.CompareTag("CraneDoor"))
        {
            isNearCraneDoor = true;
            Debug.Log("ũ������ ��ó�� ����");
        }

    }

}

