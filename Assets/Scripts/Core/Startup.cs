using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Simple script telling Unity to Destroy this if it finds an identical one in the scene!*/

public class Startup : MonoBehaviour
{

    public bool dontDestroyOnLoad = false;
    
    // Use this for initialization
    void Awake()
    {
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }
        if (GameObject.Find("Startup") != null && GameObject.Find("Startup").tag == "Startup")
        {
            Destroy(gameObject);
        }
    }
}
