using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * A Pooling System for GameObjects
*/

namespace DentedPixel
{
    public class LeanPool : object
    {
        private GameObject[] array;

        private Queue<GameObject> oldestItems;

        private int retrieveIndex = -1;

        public GameObject[] init(GameObject prefab, int count, Transform parent = null, bool retrieveOldestItems = true)
        {
            array = new GameObject[count];

            if (retrieveOldestItems)
                oldestItems = new Queue<GameObject>();

            for (int i = 0; i < array.Length; i++)
            {
                GameObject go = GameObject.Instantiate(prefab, parent);
                go.SetActive(false);

                array[i] = go;
            }

            return array;
        }

        public void init(GameObject[] array, bool retrieveOldestItems = true){
            this.array = array;

            if (retrieveOldestItems)
                oldestItems = new Queue<GameObject>();
        }

        public void giveup(GameObject go)
        {
            go.SetActive(false);
            oldestItems.Enqueue(go);
        }

        public GameObject retrieve()
        {
            for (int i = 0; i < array.Length; i++)
            {
                retrieveIndex++;
                if (retrieveIndex >= array.Length)
                    retrieveIndex = 0;

                if (array[retrieveIndex].activeSelf == false)
                {
                    GameObject returnObj = array[retrieveIndex];
                    returnObj.SetActive(true);

                    if (oldestItems != null)
                    {
                        oldestItems.Enqueue(returnObj);
                    }

                    return returnObj;
                }
            }

            if (oldestItems != null)
            {
                GameObject go = oldestItems.Dequeue();
                oldestItems.Enqueue(go);// put at the end of the queue again

                return go;
            }

            return null;
        }
    }

}