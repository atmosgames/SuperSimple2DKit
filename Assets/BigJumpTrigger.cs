using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJumpTrigger : MonoBehaviour
{

    public GameObject balloon1, balloon2;
    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (string name in GameManager.Instance.keys)
        {

            if (collision.gameObject.name == "Player" && (balloon1.active == false && balloon2.active == false))
                GameManager.Instance.EndGame("BigJump");
        }    
    }
}
