using UnityEngine;
using System.Collections;
using DentedPixel;

public class TestingRotate : MonoBehaviour {

	//method 1 leantween
	public GameObject sun ;
	public GameObject earth;

	//method 2 leantween
	public GameObject sun2;
	public GameObject earth2;

	//method 3 unity3d
	public GameObject sun3;
	public GameObject earth3;

	void Start () {

		//method 1 leantween
		Vector3 sunLocalForEarth = earth.transform.InverseTransformPoint(sun.transform.position);
		Debug.Log("sunLocalForEarth:"+sunLocalForEarth);
		LeanTween.rotateAround(earth, earth.transform.up, 360f, 5.0f).setPoint(sunLocalForEarth).setRepeat(-1);

		//method 2 leantween
		Vector3 sunLocalForEarth2 = earth2.transform.InverseTransformPoint(sun2.transform.position);
		LeanTween.rotateAroundLocal(earth2, earth2.transform.up, 360f, 5.0f).setPoint(sunLocalForEarth2);

	}

	void Update() {

		//method 3 unity3d
		earth3.transform.RotateAround(sun3.transform.position, sun3.transform.up, 72f * Time.deltaTime);

	}
}
