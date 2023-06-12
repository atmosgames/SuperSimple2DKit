using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIfDestroyed : MonoBehaviour
{
    static int x = 0;
    public int y;
    public GameObject go;

    private void OnDestroy()
    {
        x++;
        if (x == y)
        {
            go.SetActive(true);
            x = 0;
        }
            
    }
}
