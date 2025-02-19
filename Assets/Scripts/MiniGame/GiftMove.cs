using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftMove : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints; // 이동할 경로 포인트들
    [SerializeField] private float speed = 2f;

    private int currentIndex = 0;

    private void Update()
    {
        if (pathPoints.Length == 0) return;

        Vector3 targetPosition = pathPoints[currentIndex].position;

        // 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 목표 위치에 도달하면 다음 포인트로 이동
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % pathPoints.Length; // 순환 이동
        }
    }
}
