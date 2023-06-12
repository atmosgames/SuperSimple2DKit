using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DentedPixel;

public class PathSplineEndless : MonoBehaviour {
	public GameObject trackTrailRenderers;
	public GameObject car;
	public GameObject carInternal;

	public GameObject[] cubes;
	private int cubesIter;
	public GameObject[] trees;
	private int treesIter;

	public float randomIterWidth = 0.1f;

	private LTSpline track;
	private List<Vector3> trackPts = new List<Vector3>();
	private int zIter = 0;
	private float carIter = 0f;
	private float carAdd;
	private int trackMaxItems = 15;
	private int trackIter = 1;
	private float pushTrackAhead = 0f;
	private float randomIter = 0f;

	void Start () {

		// Setup initial track points
		for(int i = 0; i < 4; i++){
			addRandomTrackPoint();
		}
		refreshSpline();

		// Animate in track ahead of the car
		LeanTween.value(gameObject, 0, 0.3f, 2f).setOnUpdate( ( float val )=>{
			pushTrackAhead = val;
		});
	}
	
	void Update () {

		float zLastDist = (trackPts[ trackPts.Count - 1].z - transform.position.z);
		if(zLastDist < 200f){ // if the last node is too close we'll add in a new point and refresh the spline
			addRandomTrackPoint();
			refreshSpline();
		}

		// Update avatar's position on correct track
		track.place( car.transform, carIter );
		carIter += carAdd * Time.deltaTime;

		// we'll place the trail renders always a bit in front of the car
		track.place( trackTrailRenderers.transform, carIter + pushTrackAhead );


		// Switch tracks on keyboard input
		float turn = Input.GetAxis("Horizontal");
		if(Input.anyKeyDown){
			if(turn<0f && trackIter>0){
				trackIter--;
				playSwish();
			}else if(turn>0f && trackIter < 2){ // We have three track "rails" so stopping it from going above 3
				trackIter++;
				playSwish();
			}
			// Move the internal local x of the car to simulate changing tracks
			LeanTween.moveLocalX(carInternal, (trackIter-1)*6f, 0.3f).setEase(LeanTweenType.easeOutBack);
		}
	}

	// Simple object queuing system
	GameObject objectQueue( GameObject[] arr, ref int lastIter ){
		lastIter = lastIter>=arr.Length-1 ? 0 : lastIter+1;
		
		// Reset scale and rotation for a new animation
		arr[ lastIter ].transform.localScale = Vector3.one;
		arr[ lastIter ].transform.rotation = Quaternion.identity;
		return arr[ lastIter ];
	}

	void addRandomTrackPoint(){
		float randX = Mathf.PerlinNoise(0f, randomIter);
		randomIter += randomIterWidth;

		Vector3 randomInFrontPosition = new Vector3( (randX-0.5f)*20f, 0f, zIter*40f);

		// placing the box is just to visualize how the paths get created
		GameObject box = objectQueue( cubes, ref cubesIter ); 
		box.transform.position = randomInFrontPosition;

		// Line the roads with trees
		GameObject tree = objectQueue( trees, ref treesIter ); 
		float treeX = zIter%2==0 ? -15f : 15f;
		tree.transform.position = new Vector3( randomInFrontPosition.x + treeX, 0f, zIter*40f);

		// Animate in new tree (just for fun)
		LeanTween.rotateAround( tree, Vector3.forward, 0f, 1f).setFrom( zIter%2==0 ? 180f : -180f).setEase(LeanTweenType.easeOutBack);

		trackPts.Add( randomInFrontPosition ); // Add a future spline node
		if(trackPts.Count > trackMaxItems)
			trackPts.RemoveAt(0); // Remove the trailing spline node

		zIter++;
	}

	void refreshSpline(){
		track = new LTSpline( trackPts.ToArray() );
		carIter = track.ratioAtPoint( car.transform.position ); // we created a new spline so we need to update the cars iteration point on this new spline
		// Debug.Log("distance:"+track.distance+" carIter:"+carIter);
		carAdd = 40f / track.distance; // we want to make sure the speed is based on the distance of the spline for a more constant speed
	}

	// Make your own LeanAudio sounds at http://leanaudioplay.dentedpixel.com
	void playSwish(){
		AnimationCurve volumeCurve = new AnimationCurve( new Keyframe(0f, 0.005464481f, 1.83897f, 0f), new Keyframe(0.1114856f, 2.281785f, 0f, 0f), new Keyframe(0.2482903f, 2.271654f, 0f, 0f), new Keyframe(0.3f, 0.01670286f, 0f, 0f));
		AnimationCurve frequencyCurve = new AnimationCurve( new Keyframe(0f, 0.00136725f, 0f, 0f), new Keyframe(0.1482391f, 0.005405405f, 0f, 0f), new Keyframe(0.2650336f, 0.002480127f, 0f, 0f));

		AudioClip audioClip = LeanAudio.createAudio(volumeCurve, frequencyCurve, LeanAudio.options().setVibrato( new Vector3[]{ new Vector3(0.2f,0.5f,0f)} ).setWaveNoise().setWaveNoiseScale(1000));

		LeanAudio.play( audioClip ); //a:fvb:8,,.00136725,,,.1482391,.005405405,,,.2650336,.002480127,,,8~8,,.005464481,1.83897,,.1114856,2.281785,,,.2482903,2.271654,,,.3,.01670286,,,8~.2,.5,,~~0~~3,1000,1
	}
		
}
