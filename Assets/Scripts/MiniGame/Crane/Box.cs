using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private CraneUIManager uiManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹�ϳ���?");
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

        collision.transform.SetParent(this.transform);  // �ڽ� ������ �鰡��
        uiManager.IncreaseGiftCount();
    }

}
