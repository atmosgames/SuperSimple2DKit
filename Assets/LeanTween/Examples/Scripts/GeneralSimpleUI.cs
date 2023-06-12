using UnityEngine;
using System.Collections;
using DentedPixel;

public class GeneralSimpleUI : MonoBehaviour {
	#if !UNITY_3_5 && !UNITY_4_0 && !UNITY_4_0_1 && !UNITY_4_1 && !UNITY_4_2 && !UNITY_4_3 && !UNITY_4_5

	public RectTransform button;

	void Start () {
		Debug.Log("For better examples see the 4.6_Examples folder!");
		if(button==null){
			Debug.LogError("Button not assigned! Create a new button via Hierarchy->Create->UI->Button. Then assign it to the button variable");
			return;
		}
		
		// Tweening various values in a block callback style
		LeanTween.value(button.gameObject, button.anchoredPosition, new Vector2(200f,100f), 1f ).setOnUpdate( 
			(Vector2 val)=>{
				button.anchoredPosition = val;
			}
		);

		LeanTween.value(gameObject, 1f, 0.5f, 1f ).setOnUpdate( 
			(float volume)=>{
				Debug.Log("volume:"+volume);
			}
		);

		LeanTween.value(gameObject, gameObject.transform.position, gameObject.transform.position + new Vector3(0,1f,0), 1f ).setOnUpdate( 
			(Vector3 val)=>{
				gameObject.transform.position = val;
			}
		);

		LeanTween.value(gameObject, Color.red, Color.green, 1f ).setOnUpdate( 
			(Color val)=>{
				UnityEngine.UI.Image image = (UnityEngine.UI.Image)button.gameObject.GetComponent( typeof(UnityEngine.UI.Image) );
				image.color = val;
			}
		);

		// Tweening Using Unity's new Canvas GUI System
		LeanTween.move(button, new Vector3(200f,-100f,0f), 1f).setDelay(1f);
		LeanTween.rotateAround(button, Vector3.forward, 90f, 1f).setDelay(2f);
		LeanTween.scale(button, button.localScale*2f, 1f).setDelay(3f);
		LeanTween.rotateAround(button, Vector3.forward, -90f, 1f).setDelay(4f).setEase(LeanTweenType.easeInOutElastic);
	}

	#else
	void Start(){
		Debug.LogError("Unity 4.6+ is required to use the new UI");
	}
	
	#endif
}
