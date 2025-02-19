using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int scoreValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            MiniGameManager.Instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
