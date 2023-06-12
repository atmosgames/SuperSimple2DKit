using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBezier2d : MonoBehaviour {

	public Transform[] cubes;

	public GameObject dude1;
	public GameObject dude2;

	private LTBezierPath visualizePath;

	void Start () {
		// move 
		Vector3[] path = new Vector3[]{cubes[0].position,cubes[1].position,cubes[2].position,cubes[3].position};
		// 90 degree test
		// path = new Vector3[] {new Vector3(7.5f, 0f, 0f), new Vector3(0f, 0f, 2.5f), new Vector3(2.5f, 0f, 0f), new Vector3(0f, 0f, 7.5f)};
		visualizePath = new LTBezierPath(path);
		LeanTween.move(dude1, path, 10f).setOrientToPath2d(true);

		// move local
		LeanTween.moveLocal(dude2, path, 10f).setOrientToPath2d(true);
	}

	void OnDrawGizmos(){
		// Debug.Log("drwaing");
		Gizmos.color = Color.red;
		if(visualizePath!=null)
			visualizePath.gizmoDraw(); // To Visualize the path, use this method
	}
}
