using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Finds all of the gameObjects that have a ParallaxLayer.cs script, and moves them based on the deltaX and deltaY*/

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor; //The amount of parallax! -1 is simulates being close to the camera, 1 simulates being very far from the camera!
    [System.NonSerialized] public Vector3 newPos;
    private bool adjusted = false;

    public void Move(float deltaX, float deltaY)
    {   
        newPos = transform.localPosition;
        newPos.x -= deltaX * (parallaxFactor * 40) * (Time.deltaTime);
        newPos.y -= deltaY * (parallaxFactor * 40) * (Time.deltaTime);
        transform.localPosition = newPos;

        /*Because the camera has to lerp to it's target position the moment the level loads, 
        we wait for that to occur (1 second) while the black fader is covering the screen, 
        and then adjust the localPosition back to zero, but just one! */
        if (!adjusted) StartCoroutine(SolidifyZeroPosition());

    }

    public IEnumerator SolidifyZeroPosition()
    {
        yield return new WaitForSeconds(1f);
        transform.localPosition = Vector3.zero;
        adjusted = true;
    }
}
 