using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CraneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // 좌우 이동 속도
    [SerializeField] private float dropSpeed = 2f; // 집게 내려가는 속도
    [SerializeField] private float dropDelay = 3f; // 자동으로 집게 내려가는 시간
    private bool isDropping = false; // 집게가 내려가는 중인지 확인

    private Vector3 startPosition; // 시작 위치 저장

    [SerializeField] private GameObject joyStick; // 자동으로 집게 내려가는 시간
    [SerializeField] private float joyStickRotation;    // 회전 시간
    [SerializeField] private float joyStickRotationSpeed;   // 회전 속도

    private Transform joyStickTransform;
    private CraneManager craneManager;


    private void Start()
    {
        startPosition = transform.position; // 크레인의 초기 위치 저장
        joyStickTransform = joyStick.transform;
        craneManager = FindObjectOfType<CraneManager>();
    }

    private void Update()
    {
        if (!isDropping) // 집게가 내려가는 중이 아닐 때만 조작 가능
        {
            float move = Input.GetAxisRaw("Horizontal"); // 좌우 입력 받기
            transform.position += new Vector3(move * moveSpeed * Time.deltaTime, 0, 0);
            if (joyStickTransform != null)
            {
                float targetRz = -move * joyStickRotation;
                Quaternion targetRotation = Quaternion.Euler(0, 0, targetRz);
                joyStickTransform.rotation = Quaternion.Lerp(joyStickTransform.rotation, targetRotation, Time.deltaTime * joyStickRotationSpeed);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isDropping)
        {
            StartCoroutine(DropCrane());
        }
        if (Input.GetKeyDown(KeyCode.V))
        { 
            craneManager.ReleaseGift();
        }
    }

    // 자동으로 일정 시간 후 집게가 내려가도록 설정
    private IEnumerator AutoDropCrane()
    {
        yield return new WaitForSeconds(dropDelay);
        StartCoroutine(DropCrane());
    }

    // 집게를 내리는 코루틴
    private IEnumerator DropCrane()
    {
        isDropping = true;
        Vector3 targetPosition = transform.position + new Vector3(0, -3f, 0); // 집게가 내려갈 위치 설정

        while (transform.position.y > targetPosition.y)
        {
            transform.position -= new Vector3(0, dropSpeed * Time.deltaTime, 0);
            yield return null;
        }
        craneManager.CatchGift();
        yield return new WaitForSeconds(0.5f); // 집게가 잠시 머무름

        // 이후 집게를 다시 올리기
        StartCoroutine(RaiseCrane());
    }

    // 집게를 원래 위치로 올리는 코루틴
    private IEnumerator RaiseCrane()
    {
        Vector3 targetPosition = startPosition; // 원래 위치로 돌아가기

        while (transform.position.y < targetPosition.y)
        {
            transform.position += new Vector3(0, dropSpeed * Time.deltaTime, 0);
            yield return null;
        }

        isDropping = false;
        
    }
}
