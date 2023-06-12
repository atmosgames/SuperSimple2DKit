using UnityEngine;
using System.Collections;
using DentedPixel;

public class TestingMaxTweens : MonoBehaviour {

	private int tweenIter = 0;

	void Awake(){
		LeanTween.init (20);
	}

	void Update(){
//		Debug.Log ("tweenIter:" + tweenIter + " tweensRunning:" + LeanTween.tweensRunning + " Time:" + Time.time);
		if (tweenIter < 20) {
			GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
			Destroy( box.GetComponent( typeof(BoxCollider) ) as Component );

//			Debug.Log ("new Time.time:" + Time.time);
			LeanTween.moveX (box, 100f, 2f).setUseEstimatedTime(true).setOnComplete( ()=>{
				Debug.Log("finishes Time:"+Time.timeScale);
			}).setDelay(0.1f); 
			tweenIter++;
		}

		Time.timeScale = 0f;
	}
}
