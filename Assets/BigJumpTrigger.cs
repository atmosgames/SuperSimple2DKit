using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJumpTrigger : MonoBehaviour
{

    public GameObject balloon1, balloon2;
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player" && (balloon1.activeInHierarchy == false && balloon2.activeInHierarchy == false))
            GameManager.Instance.EndGame("BigJump");  
    }
}
