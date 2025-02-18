using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FallowCamera : MonoBehaviour
{
    public Transform target;
    public Tilemap mapBounds;
    private float minX, maxX, minY, maxY;
    [SerializeField] private float offsetX, offsetY;


    private void Start()
    {
        if (target == null || mapBounds == null) return;

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

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(target.position.x + offsetX, minX, maxX);
        pos.y = Mathf.Clamp(target.position.y + offsetY, minY, maxY);

        transform.position = pos;
    }
}
