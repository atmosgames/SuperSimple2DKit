using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class TwoBalloonsEnding : MonoBehaviour
{
    private void OnEnable()
    {
        if(GameManager.Instance.inventory.ContainsKey(ItemName.RedBalloon))
        {
            StartCoroutine("ChangeScene");
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.EndGame("2Balloons");
    }
}
