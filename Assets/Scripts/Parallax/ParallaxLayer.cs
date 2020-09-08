using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Allows the controller to move each layer based on the parallaxAmount!*/

public class ParallaxLayer : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float parallaxAmount; //The amount of parallax! 1 simulates being close to the camera, -1 simulates being very far from the camera!
    [System.NonSerialized] public Vector3 newPosition;
    private bool adjusted = false;

    public void MoveLayer(float positionChangeX, float positionChangeY)
    {
        newPosition = transform.localPosition;
        newPosition.x -= positionChangeX * (-parallaxAmount * 40) * (Time.deltaTime);
        newPosition.y -= positionChangeY * (-parallaxAmount * 40) * (Time.deltaTime);
        transform.localPosition = newPosition;
    }

}
