using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float highPosY = 1f;
    public float lowPosY = -1f;

    public float holeSizeMin = 1f;
    public float holeSizeMax = 3f;

    public Transform topObject;
    public Transform bottomObject;

    public float widthPadding = 4f;

    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private GameObject coinPrefab;

    public float coinSpawnPercent = 0.5f;

    GameManager gameManager;

    public void Start()
    {
        gameManager = GameManager.Instance;
    }
    
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        SpriteRenderer childRenderer = GetComponentInChildren<SpriteRenderer>();
        int randomIndex = Random.Range(0, 2);
        childRenderer.sprite = (randomIndex == 0) ? sprite1 : sprite2;

        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;

        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, halfHoleSize -6.0f);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        if (coinPrefab != null && Random.value < coinSpawnPercent)
        { 
            float randomYOffset = Random.Range(2.0f, 3.5f);
            Vector3 coinPosition = topObject.position + new Vector3(0, randomYOffset, 0);
            GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);
        }

        return placePosition;
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    Player player = other.GetComponent<Player>();
    //    if (player != null)
    //        gameManager.AddScore(1);
    //}
}
