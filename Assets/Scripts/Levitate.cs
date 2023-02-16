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
        start = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, start + Mathf.Sin(Time.time * speed) * height,transform.localPosition.z);
    }
}
