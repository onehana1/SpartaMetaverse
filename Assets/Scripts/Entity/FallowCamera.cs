using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallowCamera : MonoBehaviour
{
    public Transform target;
    public Tilemap mapBounds;
    private float minX, maxX, minY, maxY;
    [SerializeField] private float offsetX, offsetY;
    [SerializeField] private float smoothSpeed = 5f; // 카메라 이동 속도
    [SerializeField] private bool fallowX  = true;
    [SerializeField] private bool fallowY = true;



    private void Start()
    {
        if (mapBounds == null) return;

        offsetX = transform.position.x - target.position.x;

        Bounds bounds = mapBounds.localBounds;
        float camHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float camHalfHeight = Camera.main.orthographicSize;

        minX = bounds.min.x + camHalfWidth;
        maxX = bounds.max.x - camHalfWidth;
        minY = bounds.min.y + camHalfHeight;
        maxY = bounds.max.y - camHalfHeight;

    }

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = transform.position;

        if (fallowX)
        {
            targetPosition.x = target.position.x + offsetX;
            if (mapBounds != null)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            }
        }

        if (fallowY)
        {
            targetPosition.y = target.position.y + offsetY;
            if (mapBounds != null)
            {
                targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
            }
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
