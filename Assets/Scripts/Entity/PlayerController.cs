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


        // 이동 중일 때만 바라보는 방향을 업데이트

        if (isShift)
        {
            movementDirection = Vector2.zero;

            // movementDirection 얘가 0이면 방향 못구함 그래서 새로운 변수 만들어서 방향 구하기
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
        if (other.CompareTag("HouseDoor")) // House의 Collision 오브젝트에 Tag 설정 필요
        {
            isNearDoor = true;
            Debug.Log("집 근처에 있음");
        }
    }

}

