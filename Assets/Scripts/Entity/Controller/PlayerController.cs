using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    public bool isNearDoor = false;
    public bool isNearTomstone = false;
    public bool isNearCraneDoor = false;
    [SerializeField] private GameObject playerImage;
    [SerializeField] private GameObject RidePivot;
    private bool isRide = false;



    protected override void Start()
    {
        base.Start();
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
            GameEventManager.TriggerEnterDoor(true);
        }

        if (isNearTomstone && Input.GetKeyDown(KeyCode.F))
        {
            GameEventManager.TriggerEnterTomstone(true);
        }

        if (isNearCraneDoor && Input.GetKeyDown(KeyCode.F))
        {
            GameEventManager.TriggerEnterCraneDoor(true);
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
            Debug.Log("집 근처에 있음");
        }
        if (other.CompareTag("Tombstone")) 
        {
            isNearTomstone = true;
            Debug.Log("비석 근처에 있음");
        }
        if (other.CompareTag("CraneDoor"))
        {
            isNearCraneDoor = true;
            Debug.Log("크레인집 근처에 있음");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HouseDoor"))
        {
            isNearDoor = false;
        }
        if (other.CompareTag("Tombstone"))
        {
            isNearTomstone = false;
        }
        if (other.CompareTag("CraneDoor"))
        {
            isNearCraneDoor = false;
        }
        
    }

}

