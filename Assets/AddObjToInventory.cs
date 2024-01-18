using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameManager;
public class AddObjToInventory : MonoBehaviour
{
    public ItemName invName;
    [SerializeField] private Sprite icon;
    [SerializeField] private ItemName toRemove;
    private void OnEnable()
    {
        if (toRemove != ItemName.None)
            GameManager.Instance.RemoveInventoryItem(toRemove);
        GameManager.Instance.GetInventoryItem(invName,icon);
    }
}
