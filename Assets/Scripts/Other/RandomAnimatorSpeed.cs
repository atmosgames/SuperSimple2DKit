using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Attach this to any object with an Animator component. It will set the animation speed to a random float between the hi and low values you set in the inspector!*/

public class RandomAnimatorSpeed : MonoBehaviour
{

    private Animator animator;
    [SerializeField] float low = .3f;
    [SerializeField] float high = 1.5f;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>() as Animator;
        animator.speed = Random.Range(low, high);
    }
}
