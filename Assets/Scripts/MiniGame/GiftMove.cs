using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiftMove : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints; // �̵��� ��� ����Ʈ��
    [SerializeField] private float speed = 2f;

    private int currentIndex = 0;
    private bool isCathed =false;

    private void Update()
    {
        if (isCathed || pathPoints.Length == 0) return;

        Vector3 targetPosition = pathPoints[currentIndex].position;

        // �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // ��ǥ ��ġ�� �����ϸ� ���� ����Ʈ�� �̵�
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentIndex = (currentIndex + 1) % pathPoints.Length; // ��ȯ �̵�
        }
    }

    public void catchGift(Vector3 newPos)
    {
        isCathed=true;
        transform.position = newPos;

    }
    public void ReleaseGift(Vector3 newPos)
    {
        isCathed = false;
        transform.position = newPos;
    }
    public void StartMove()
    {
        isCathed=false;
    }
}
