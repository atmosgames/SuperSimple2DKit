using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Rotates a 2D gameObject!*/ 

public class RotateObject : MonoBehaviour
{

    [SerializeField] float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * Time.deltaTime * speed);
    }
}
