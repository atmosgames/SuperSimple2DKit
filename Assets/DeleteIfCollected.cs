using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteIfCollected : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(gameObject.scene.name + gameObject.name) == 1)
            Destroy(gameObject);
    }

}
