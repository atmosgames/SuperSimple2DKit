using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItemFromInventory : MonoBehaviour
{
    public string invName = "LightBeer";

    private void OnEnable()
    {
        GameManager.Instance.RemoveInventoryItem(invName);
    }
}
