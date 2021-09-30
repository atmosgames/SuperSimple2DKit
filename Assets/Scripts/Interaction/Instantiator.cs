using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Instantiates (creates) one or more objects*/

public class Instantiator : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] float amount; //Amount can only be used if objects is one item

    public void InstantiateObjects()
    {
        GameObject iObject;

        //Instantiate the entire array of objects
        if (amount == 0)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                iObject = Instantiate(objects[i], transform.position, Quaternion.identity, null);
                if (iObject.GetComponent<Ejector>() != null)
                {
                    iObject.GetComponent<Ejector>().launchOnStart = true;
                }
            }
        }

        //Instantiate a specific amount of the first object in the array
        else if (objects.Length != 0)
        {
            for (int i = 0; i < amount; i++)
            {
                iObject = Instantiate(objects[0], transform.position, Quaternion.identity, null);
                if (iObject.GetComponent<Ejector>() != null)
                {
                    iObject.GetComponent<Ejector>().launchOnStart = true;
                }
            }

        }
    }
}
