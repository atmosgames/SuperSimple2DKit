using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfEnding : MonoBehaviour
{
    [SerializeField] private string ending;
    [SerializeField] private int ifTrue;

    private void Start()
    {
        if(ifTrue != GameManager.Instance.gameCompletion[ending])
        {
            gameObject.SetActive(false);
        }
    }
}
