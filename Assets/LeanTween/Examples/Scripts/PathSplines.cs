using UnityEngine;
using System.Collections;
using DentedPixel;

public class PathSplines : MonoBehaviour {

	public Transform[] trans;
	
	LTSpline cr;
	private GameObject avatar1;

	void OnEnable(){
		// create the path
		cr = new LTSpline( new Vector3[] {trans[0].position, trans[1].position, trans[2].position, trans[3].position, trans[4].position} );
		// cr = new LTSpline( new Vector3[] {new Vector3(-1f,0f,0f), new Vector3(0f,0f,0f), new Vector3(4f,0f,0f), new Vector3(20f,0f,0f), new Vector3(30f,0f,0f)} );
	}

	void Start () {
		avatar1 = GameObject.Find("Avatar1");

		// Tween automatically
		LeanTween.move(avatar1, cr, 6.5f).setOrientToPath(true).setRepeat(1).setOnComplete( ()=>{
			Vector3[] next = new Vector3[] {trans[4].position, trans[3].position, trans[2].position, trans[1].position, trans[0].position};
			LeanTween.moveSpline( avatar1, next, 6.5f); // move it back to the start without an LTSpline
		}).setEase(LeanTweenType.easeOutQuad);
	}
	
	private float iter;
	void Update () {
		// Or Update Manually
		// cr.place( avatar1.transform, iter );

		iter += Time.deltaTime*0.07f;
		if(iter>1.0f)
			iter = 0.0f;
	}

	void OnDrawGizmos(){
		// Debug.Log("drwaing");
		if(cr==null)
			OnEnable();
		Gizmos.color = Color.red;
		if(cr!=null)
			cr.gizmoDraw(); // To Visualize the path, use this method
	}
}
