using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DentedPixel;

public class LogoCinematic : MonoBehaviour {

	public GameObject lean;

	public GameObject tween;

	void Awake(){
		
	}

	
	void Start () {
		//Time.timeScale = 0.2f;
		
		// Slide in
		tween.transform.localPosition += -Vector3.right * 15f;
		LeanTween.moveLocalX(tween, tween.transform.localPosition.x+15f, 0.4f).setEase(LeanTweenType.linear).setDelay(0f).setOnComplete( playBoom );

		// Drop Down tween down
		tween.transform.RotateAround(tween.transform.position, Vector3.forward, -30f);
		LeanTween.rotateAround(tween, Vector3.forward, 30f, 0.4f).setEase(LeanTweenType.easeInQuad).setDelay(0.4f).setOnComplete( playBoom );

		// Drop Lean In
		lean.transform.position += Vector3.up * 5.1f;
		LeanTween.moveY(lean, lean.transform.position.y-5.1f, 0.6f).setEase(LeanTweenType.easeInQuad).setDelay(0.6f).setOnComplete( playBoom );
	}

	void playBoom(){
		// Make your own Dynamic Audio at http://leanaudioplay.dentedpixel.com
		
		AnimationCurve volumeCurve = new AnimationCurve( new Keyframe(0f, 1.163155f, 0f, -1f), new Keyframe(0.3098361f, 0f, 0f, 0f), new Keyframe(0.5f, 0.003524712f, 0f, 0f));
		AnimationCurve frequencyCurve = new AnimationCurve( new Keyframe(0.000819672f, 0.007666667f, 0f, 0f), new Keyframe(0.01065573f, 0.002424242f, 0f, 0f), new Keyframe(0.02704918f, 0.007454545f, 0f, 0f), new Keyframe(0.03770492f, 0.002575758f, 0f, 0f), new Keyframe(0.052459f, 0.007090909f, 0f, 0f), new Keyframe(0.06885245f, 0.002939394f, 0f, 0f), new Keyframe(0.0819672f, 0.006727273f, 0f, 0f), new Keyframe(0.1040983f, 0.003181818f, 0f, 0f), new Keyframe(0.1188525f, 0.006212121f, 0f, 0f), new Keyframe(0.145082f, 0.004151515f, 0f, 0f), new Keyframe(0.1893443f, 0.005636364f, 0f, 0f));

		AudioClip audioClip = LeanAudio.createAudio(volumeCurve, frequencyCurve, LeanAudio.options().setVibrato( new Vector3[]{ new Vector3(0.1f,0f,0f)} ).setFrequency(11025));

		LeanAudio.play( audioClip ); //a:fvb:8,.000819672,.007666667,,,.01065573,.002424242,,,.02704918,.007454545,,,.03770492,.002575758,,,.052459,.007090909,,,.06885245,.002939394,,,.0819672,.006727273,,,.1040983,.003181818,,,.1188525,.006212121,,,.145082,.004151515,,,.1893443,.005636364,,,8~8,,1.163155,,-,.3098361,,,,.5,.003524712,,,8~.1,,,~11025~0~~
	}
	
}
