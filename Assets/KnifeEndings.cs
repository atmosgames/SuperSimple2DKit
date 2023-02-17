using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeEndings : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Hacker")
            GameManager.Instance.EndGame("HackerAttack");
        else if(collision.gameObject.name == "Seller")
            GameManager.Instance.EndGame("SellerAttack");

    }
}
