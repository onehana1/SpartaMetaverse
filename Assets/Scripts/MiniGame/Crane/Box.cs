using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private CraneUIManager uiManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌하나요?");
        if (collision.CompareTag("Gift"))
        {
            if (uiManager != null)
            {
                uiManager.IncreaseGiftCount();
            }
        }

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.isKinematic = true;
        }

        collision.transform.SetParent(this.transform);  // 박스 안으로 들가게
        uiManager.IncreaseGiftCount();
    }

}
