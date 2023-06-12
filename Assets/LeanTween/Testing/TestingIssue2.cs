using UnityEngine;
using System.Collections;
using DentedPixel;

public class TestingIssue2 : MonoBehaviour {
	public RectTransform rect;
	public GameObject go;
	public GameObject go2;

	private LTDescr descr;

	void Start () {
		descr = LeanTween.move(go, new Vector3(0f,0,100f), 10f);
		descr.passed = 5f; // this should put it at the midway
		descr.updateNow();
		descr.pause(); // doesn't matter if pause after or before setting descr.passed I think if I set the passed property and paused the next frame it would work

//		LeanTween.scale(go2, Vector3.one * 4f, 10f).setEasePunch();

		LeanTween.scaleX (go2, (go2.transform.localScale * 1.5f).x, 15f).setEase (LeanTweenType.punch);
		LeanTween.scaleY (go2, (go2.transform.localScale * 1.5f).y, 15f).setEase (LeanTweenType.punch);
		LeanTween.scaleZ (go2, (go2.transform.localScale * 1.5f).z, 15f).setEase (LeanTweenType.punch);
	}
	bool set = false;
	void Update () {
		if (Time.unscaledTime > 5f && !set)
		{
			set = true;
			descr.resume(); // once this execute the object is put at the midway position as setted by passed and the tween continue.
			Debug.Log("resuming");
		}
	}
}
