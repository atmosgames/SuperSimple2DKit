using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    [SerializeField]
    private float height;
    [SerializeField]
    private float speed;

    private float start;
    void Start()
    {
        start = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, start + Mathf.Sin(Time.time * speed) * height,transform.position.z);
    }
}
