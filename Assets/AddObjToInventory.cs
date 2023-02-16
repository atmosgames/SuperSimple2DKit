using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjToInventory : MonoBehaviour
{
    public string invName;
    
    [SerializeField] private Sprite icon;

    private void OnEnable()
    {
        GameManager.Instance.GetInventoryItem(invName,icon);
    }
}
