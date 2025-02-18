using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseController
{
    private Camera camera;

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

    }
}

