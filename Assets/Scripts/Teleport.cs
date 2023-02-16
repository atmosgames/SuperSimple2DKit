using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource source;

    void OnEnable()
    {
        player.transform.position = transform.position;
        
        if(source)
            source.Play();

        gameObject.SetActive(false);
    }
}
