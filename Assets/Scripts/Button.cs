using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public AudioSource myFx;
    public AudioClip hoverFx;

    public void HoverSound()
    {
        myFx.PlayOneShot(hoverFx);
    }
}
