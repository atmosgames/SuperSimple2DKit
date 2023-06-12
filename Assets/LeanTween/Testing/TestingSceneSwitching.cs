using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DentedPixel;

public class TestingSceneSwitching : MonoBehaviour {

	public GameObject cube;

	private static int sceneIter = 0;

	private int tweenCompleteCnt;

	// Use this for initialization
	void Start () {
		LeanTest.expected = 6;
		
		// Start a couple of tweens and make sure they complete
		tweenCompleteCnt = 0;

		LeanTween.scale(cube, new Vector3(3f,3f,3f), 0.1f).setDelay(0.1f).setOnComplete( ()=>{
			tweenCompleteCnt++;
		});

		LeanTween.move(cube, new Vector3(3f,3f,3f), 0.1f).setOnComplete( ()=>{
			tweenCompleteCnt++;
		});

		LeanTween.delayedCall(cube, 0.1f, ()=>{
			tweenCompleteCnt++;
		});

		// Schedule a couple of tweens, make sure some only half complete than switch scenes

		LeanTween.delayedCall(cube, 1f, ()=>{
			LeanTween.scale(cube, new Vector3(3f,3f,3f), 1f).setDelay(0.1f).setOnComplete( ()=>{

			});

			LeanTween.move(cube, new Vector3(3f,3f,3f), 1f).setOnComplete( ()=>{

			});
		});

		// Load next scene
		LeanTween.delayedCall(cube, 0.5f, ()=>{
			LeanTest.expect( tweenCompleteCnt==3, "Scheduled tweens completed:"+sceneIter);
			if(sceneIter<5){
				sceneIter++;
				SceneManager.LoadScene(0);
			}
		});
	}
	
	
}
