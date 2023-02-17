using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObjToInventory : MonoBehaviour
{
    public string invName;
    [SerializeField] private Sprite icon;
    [SerializeField] private string toRemove;
    private void OnEnable()
    {
        if (toRemove != "")
            GameManager.Instance.RemoveInventoryItem(toRemove);
        GameManager.Instance.GetInventoryItem(invName,icon);
    }
}
