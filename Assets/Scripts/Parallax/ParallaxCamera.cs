using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Takes information from the camera, and calculates and old position, and a new one, thus getting a delta value*/

public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float deltaMovementX, float deltaMovementY);
    public ParallaxCameraDelegate onCameraTranslate;
    private float oldPositionX;
    private float oldPositionY;

    void Start()
    {
        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;
    }
    void Update()
    {
        if (transform.position.x != oldPositionX || (transform.position.y) != oldPositionY)
        {
            if (onCameraTranslate != null)
            {
                float deltaX = oldPositionX - transform.position.x;
                float deltaY = oldPositionY - transform.position.y;
                onCameraTranslate(deltaX, deltaY);
            }

            oldPositionX = transform.position.x;
            oldPositionY = transform.position.y;

          
        }
    }
}