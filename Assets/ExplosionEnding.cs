using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEnding : MonoBehaviour
{
    [SerializeField] GameObject bombGraphics;
    [SerializeField] GameObject playerSprite;

    private void Start()
    {
        bombGraphics.SetActive(false);
        playerSprite.SetActive(false);
    }
}
