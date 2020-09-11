using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Finds all of the gameObjects that have a ParallaxLayer.cs script, and moves them!*/

public class ParallaxController : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(float cameraPositionChangeX, float cameraPositionChangeY);
    public ParallaxCameraDelegate onCameraMove;
    private Vector2 oldCameraPosition;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        onCameraMove += MoveLayer;
        FindLayers();
        oldCameraPosition.x = Camera.main.transform.position.x;
        oldCameraPosition.y = Camera.main.transform.position.y;
    }

    private void FixedUpdate()
    {
        if (Camera.main.transform.position.x != oldCameraPosition.x || (Camera.main.transform.position.y) != oldCameraPosition.y)
        {
            if (onCameraMove != null)
            {
                Vector2 cameraPositionChange;
                cameraPositionChange = new Vector2(oldCameraPosition.x - Camera.main.transform.position.x, oldCameraPosition.y - Camera.main.transform.position.y);
                onCameraMove(cameraPositionChange.x, cameraPositionChange.y);
            }

            oldCameraPosition = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
        }
    }

    //Finds all the objects that have a ParallaxLayer component, and adds them to the parallaxLayers list.
    void FindLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                parallaxLayers.Add(layer);
            }
        }
    }

    //Move each layer based on each layers position. This is being used via the ParallaxLayer script
    void MoveLayer(float positionChangeX, float positionChangeY)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.MoveLayer(positionChangeX, positionChangeY);
        }
    }
}