using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSequencer : MonoBehaviour {

	public GameObject avatar1;

    public GameObject star;

	public GameObject dustCloudPrefab;

	public float speedScale = 1f;

	public void Start(){

		// Jump up
		var seq = LeanTween.sequence();


		seq.append( LeanTween.moveY( avatar1, avatar1.transform.localPosition.y + 6f, 1f).setEaseOutQuad() );

        // Power up star, use insert when you want to branch off from the regular sequence (this does not push back the delay of other subsequent tweens)
        seq.insert( LeanTween.alpha(star, 0f, 1f) );
        seq.insert( LeanTween.scale( star, Vector3.one * 3f, 1f) );

		// Rotate 360
		seq.append( LeanTween.rotateAround( avatar1, Vector3.forward, 360f, 0.6f ).setEaseInBack() );

		// Return to ground
		seq.append( LeanTween.moveY( avatar1, avatar1.transform.localPosition.y, 1f).setEaseInQuad() );

		// Kick off spiraling clouds - Example of appending a callback method
		seq.append(() => {
			for(int i = 0; i < 50f; i++){
				GameObject cloud = Instantiate(dustCloudPrefab) as GameObject;
				cloud.transform.parent = avatar1.transform;
				cloud.transform.localPosition = new Vector3(Random.Range(-2f,2f),0f,0f);
				cloud.transform.eulerAngles = new Vector3(0f,0f,Random.Range(0,360f));

				var range = new Vector3(cloud.transform.localPosition.x, Random.Range(2f,4f), Random.Range(-10f,10f));

				// Tweens not in a sequence, because we want them all to animate at the same time
				LeanTween.moveLocal(cloud, range, 3f*speedScale).setEaseOutCirc();
				LeanTween.rotateAround(cloud, Vector3.forward, 360f*2, 3f*speedScale).setEaseOutCirc();
				LeanTween.alpha(cloud, 0f, 3f*speedScale).setEaseOutCirc().setDestroyOnComplete(true);
			}
		});

		// You can speed up or slow down the sequence of events
		seq.setScale(speedScale);

        // seq.reverse(); // not working yet

        // Testing canceling sequence after a bit of time
        //LeanTween.delayedCall(3f, () =>
        //{
        //    LeanTween.cancel(seq.id);
        //});
	}
}
