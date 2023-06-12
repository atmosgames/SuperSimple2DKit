using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPlaceholder : MonoBehaviour
{
    [SerializeField] private Ending ending;

    private void Awake()
    {
        if(PlayerPrefs.GetInt(ending.name, 0) == 1 && ending.icon )
            GetComponent<Image>().sprite = ending.icon;
    }
}
