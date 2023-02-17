using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueEndingTrigger : MonoBehaviour
{

    [SerializeField] private string endingName;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name != "Player") return;

        GameManager.Instance.EndGame(endingName);
    }
}
