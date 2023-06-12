using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDescr : MonoBehaviour {

	private int tweenId;

	public GameObject go;

	// start a tween
	public void startTween(){
		tweenId = LeanTween.moveX(go, 10f, 1f).id;
		Debug.Log("tweenId:" + tweenId);
	}

	// check tween descr
	public void checkTweenDescr(){
		var descr = LeanTween.descr(tweenId);
		Debug.Log("descr:" + descr);
		Debug.Log("isTweening:"+LeanTween.isTweening(tweenId));
	}
}
