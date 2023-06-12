using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingOptionalCancel : MonoBehaviour {

    public GameObject cube1;

	// Use this for initialization
	void Start () {
        LeanTween.init(1);
        // Fire up a bunch with onUpdates
        LeanTween.moveX(cube1, 10f, 1f).setOnUpdate((float val) =>
        {
            Debug.Log("on update.... val:"+val+" cube1.x:"+cube1.transform.position.x);
        });

	}

    private bool alternate = true;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            LeanTween.moveX(cube1, alternate ? -10f : 10f, 1f).setOnUpdate((float val) =>
            {
                Debug.Log("2 on update.... val:" + val + " cube1.x:" + cube1.transform.position.x);
            });
            alternate = !alternate;
        }
    }
}
