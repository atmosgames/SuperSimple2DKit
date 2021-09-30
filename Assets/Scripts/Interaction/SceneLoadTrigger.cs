using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Loads a new scene, while also clearing level-specific inventory!*/

public class SceneLoadTrigger : MonoBehaviour
{

    [SerializeField] string loadSceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            GameManager.Instance.hud.loadSceneName = loadSceneName;
            GameManager.Instance.inventory.Clear();
            GameManager.Instance.hud.animator.SetTrigger("coverScreen");
            enabled = false;
        }
    }
}
