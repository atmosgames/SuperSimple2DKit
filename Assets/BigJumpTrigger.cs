using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigJumpTrigger : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            GameManager.Instance.EndGame("BigJump");
    }

}
