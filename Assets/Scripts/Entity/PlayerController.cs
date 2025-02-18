using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    private Camera camera;
    private bool isNearDoor = false;

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
            SceneManager.LoadScene("MiniGameScene");
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HouseDoor")) // House�� Collision ������Ʈ�� Tag ���� �ʿ�
        {
            isNearDoor = true;
            Debug.Log("�� ��ó�� ����");
        }
    }

}

