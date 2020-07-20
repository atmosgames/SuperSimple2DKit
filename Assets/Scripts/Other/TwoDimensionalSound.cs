using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Although Unity has a built-in 2D sound system, this gives us a bit more control. Add to any object with an audioSource to control the volume based on it's position to the player!*/
public class TwoDimensionalSound : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] float randomPitchAdder = 0;
    [SerializeField] float range;
    [SerializeField] float volume;
    [SerializeField] Vector3 distanceBetweenPlayer;
    [SerializeField] float magnitude;
    
    // Use this for initialization
    void Start()
    {
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
        audioSource.pitch += Random.Range(-randomPitchAdder / 3, randomPitchAdder);
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenPlayer = transform.position - NewPlayer.Instance.transform.position;
        magnitude = (range - distanceBetweenPlayer.magnitude) / range;
        if (magnitude <= 1)
        {
            audioSource.volume = magnitude;
        }
        else
        {
            audioSource.volume = 1;
        }
    }
}
