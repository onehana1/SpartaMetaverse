using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneManager : MonoBehaviour
{
    [SerializeField] private CraneController crane; 
    [SerializeField] private float offsetY = -0.5f;
    [SerializeField] private float dropY = -3.0f; 

    private GiftMove gift; 
    private bool isHold = false;
    private CraneUIManager craneUIManager;
    public void CatchGift()
    {
        if (gift == null) return; 
        isHold = true;
        gift.catchGift(crane.transform.position + new Vector3(0, offsetY, 0)); 
        gift.transform.SetParent(crane.transform);
       // craneUIManager.IncreaseGiftCount();
    }


    public void ReleaseGift()
    {
        if (gift == null) return; 
                isHold = false;

        gift.transform.SetParent(null);
        gift.ReleaseGift(crane.transform.position + new Vector3(0, dropY, 0));

        gift.StartMove();


        gift = null; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("부딪히긴하는데");
        if (!isHold && collision.CompareTag("Gift"))
        {
            isHold = true;
            gift = collision.GetComponent<GiftMove>();
            gift.enabled = false;
        }
    }
}
