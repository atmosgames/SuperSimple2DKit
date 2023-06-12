using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing248 : MonoBehaviour {

    public GameObject dude1;

	// Use this for initialization
	void Start () {
        //dude1.LeanMoveX(10f, 1f);
        int id = LeanTween.moveX(dude1, 1f, 3f).id;
        Debug.Log("id:" + id);
        if (LeanTween.isTweening(id))
            Debug.Log("I am tweening!");
	}
}
