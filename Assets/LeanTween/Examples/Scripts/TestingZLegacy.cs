using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using DentedPixel;

public class TestingZLegacy : MonoBehaviour {
	public AnimationCurve customAnimationCurve;
	public Transform pt1;
	public Transform pt2;
	public Transform pt3;
	public Transform pt4;
	public Transform pt5;
	
	public delegate void NextFunc();
	private int exampleIter = 0;
	private string[] exampleFunctions = new string[] { /**/"updateValue3Example", "loopTestClamp", "loopTestPingPong", "moveOnACurveExample", "customTweenExample", "moveExample", "rotateExample", "scaleExample", "updateValueExample", "delayedCallExample", "alphaExample", "moveLocalExample", "rotateAroundExample", "colorExample" };
	public bool useEstimatedTime = true;
	private GameObject ltLogo;
    private TimingType timingType = TimingType.HalfTimeScale;
	private int descrTimeScaleChangeId;
	private Vector3 origin;

	public enum TimingType{
		SteadyNormalTime,
		IgnoreTimeScale,
		HalfTimeScale,
		VariableTimeScale,
		Length
	}

	void Awake(){
		LeanTween.init(3200); // This line is optional. Here you can specify the maximum number of tweens you will use (the default is 400).  This must be called before any use of LeanTween is made for it to be effective.
	}

	void Start () {
		ltLogo = GameObject.Find("LeanTweenLogo");
		LeanTween.delayedCall(1f, cycleThroughExamples);
		origin = ltLogo.transform.position;

//		alphaExample();
	}

	void pauseNow(){
		Time.timeScale = 0f;
		Debug.Log("pausing");
	}

	void OnGUI(){
		string label = useEstimatedTime ? "useEstimatedTime" : "timeScale:"+Time.timeScale;
		GUI.Label(new Rect(0.03f*Screen.width,0.03f*Screen.height,0.5f*Screen.width,0.3f*Screen.height), label);
	}
	
	void endlessCallback(){
		Debug.Log("endless");
	}

	void cycleThroughExamples(){
		if(exampleIter==0){
			int iter = (int)timingType + 1;
			if(iter>(int)TimingType.Length)
				iter = 0;
			timingType = (TimingType)iter;
			useEstimatedTime = timingType==TimingType.IgnoreTimeScale;
			Time.timeScale = useEstimatedTime ? 0 : 1f; // pause the Time Scale to show the effectiveness of the useEstimatedTime feature (this is very usefull with Pause Screens)
			if(timingType==TimingType.HalfTimeScale)
				Time.timeScale = 0.5f;

			if(timingType==TimingType.VariableTimeScale){
				descrTimeScaleChangeId = LeanTween.value( gameObject, 0.01f, 10.0f, 3f).setOnUpdate( (float val)=>{
					//Debug.Log("timeScale val:"+val);
					Time.timeScale = val;
				}).setEase(LeanTweenType.easeInQuad).setUseEstimatedTime(true).setRepeat(-1).id;
			}else{
				Debug.Log("cancel variable time");
				LeanTween.cancel( descrTimeScaleChangeId );
			}
		}
		gameObject.BroadcastMessage( exampleFunctions[ exampleIter ] );

		// Debug.Log("cycleThroughExamples time:"+Time.time + " useEstimatedTime:"+useEstimatedTime);
		float delayTime = 1.1f;
		LeanTween.delayedCall( gameObject, delayTime, cycleThroughExamples).setUseEstimatedTime(useEstimatedTime);

		exampleIter = exampleIter+1>=exampleFunctions.Length ? 0 : exampleIter + 1;
	}

	public void updateValue3Example(){
		Debug.Log("updateValue3Example Time:"+Time.time);
		LeanTween.value( gameObject, updateValue3ExampleCallback, new Vector3(0.0f, 270.0f, 0.0f), new Vector3(30.0f, 270.0f, 180f), 0.5f ).setEase(LeanTweenType.easeInBounce).setRepeat(2).setLoopPingPong().setOnUpdateVector3(updateValue3ExampleUpdate).setUseEstimatedTime(useEstimatedTime);
	}

	public void updateValue3ExampleUpdate( Vector3 val){
		//Debug.Log("val:"+val+" obj:"+obj);
	}

	public void updateValue3ExampleCallback( Vector3 val ){
		ltLogo.transform.eulerAngles = val;
		// Debug.Log("updateValue3ExampleCallback:"+val);
	}

	public void loopTestClamp(){
		Debug.Log("loopTestClamp Time:"+Time.time);
		GameObject cube1 = GameObject.Find("Cube1");
		cube1.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		LeanTween.scaleZ( cube1, 4.0f, 1.0f).setEase(LeanTweenType.easeOutElastic).setRepeat(7).setLoopClamp().setUseEstimatedTime(useEstimatedTime);//
	}

	public void loopTestPingPong(){
		Debug.Log("loopTestPingPong Time:"+Time.time);
		GameObject cube2 = GameObject.Find("Cube2");
		cube2.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		LeanTween.scaleY( cube2, 4.0f, 1.0f ).setEase(LeanTweenType.easeOutQuad).setLoopPingPong(4).setUseEstimatedTime(useEstimatedTime);
		//LeanTween.scaleY( cube2, 4.0f, 1.0f, LeanTween.options().setEaseOutQuad().setRepeat(8).setLoopPingPong().setUseEstimatedTime(useEstimatedTime) );
	}

	public void colorExample(){
		GameObject lChar = GameObject.Find("LCharacter");
		LeanTween.color( lChar, new Color(1.0f,0.0f,0.0f,0.5f), 0.5f ).setEase(LeanTweenType.easeOutBounce).setRepeat(2).setLoopPingPong().setUseEstimatedTime(useEstimatedTime);
	}

	public void moveOnACurveExample(){
		Debug.Log("moveOnACurveExample Time:"+Time.time);

		Vector3[] path = new Vector3[] { origin,pt1.position,pt2.position,pt3.position,pt3.position,pt4.position,pt5.position,origin};
		LeanTween.move( ltLogo, path, 1.0f ).setEase(LeanTweenType.easeOutQuad).setOrientToPath(true).setUseEstimatedTime(useEstimatedTime);
	}
	
	public void customTweenExample(){
		Debug.Log("customTweenExample starting pos:"+ltLogo.transform.position+" origin:"+origin);
		
		LeanTween.moveX( ltLogo, -10.0f, 0.5f ).setEase(customAnimationCurve).setUseEstimatedTime(useEstimatedTime);
		LeanTween.moveX( ltLogo, 0.0f, 0.5f ).setEase(customAnimationCurve).setDelay(0.5f).setUseEstimatedTime(useEstimatedTime);
	}
	
	public void moveExample(){
		Debug.Log("moveExample");
		
		LeanTween.move( ltLogo, new Vector3(-2f,-1f,0f), 0.5f).setUseEstimatedTime(useEstimatedTime);
		LeanTween.move( ltLogo, origin, 0.5f).setDelay(0.5f).setUseEstimatedTime(useEstimatedTime);
	}
	
	public void rotateExample(){
		Debug.Log("rotateExample");

		Hashtable returnParam = new Hashtable();
		returnParam.Add("yo", 5.0);
		
		LeanTween.rotate( ltLogo, new Vector3(0f,360f,0f), 1f).setEase(LeanTweenType.easeOutQuad).setOnComplete(rotateFinished).setOnCompleteParam(returnParam).setOnUpdate(rotateOnUpdate).setUseEstimatedTime(useEstimatedTime);
	}

	public void rotateOnUpdate( float val ){
		//Debug.Log("rotating val:"+val);
	}

	public void rotateFinished( object hash ){
		Hashtable h = hash as Hashtable;
		Debug.Log("rotateFinished hash:"+h["yo"]);
	}
	
	public void scaleExample(){
		Debug.Log("scaleExample");
		
		Vector3 currentScale = ltLogo.transform.localScale;
		LeanTween.scale( ltLogo, new Vector3(currentScale.x+0.2f,currentScale.y+0.2f,currentScale.z+0.2f), 1f ).setEase(LeanTweenType.easeOutBounce).setUseEstimatedTime(useEstimatedTime);
	}
	
	public void updateValueExample(){
		Debug.Log("updateValueExample");
		Hashtable pass = new Hashtable();
		pass.Add("message", "hi");
		LeanTween.value( gameObject, (Action<float, object>)updateValueExampleCallback, ltLogo.transform.eulerAngles.y, 270f, 1f ).setEase(LeanTweenType.easeOutElastic).setOnUpdateParam(pass).setUseEstimatedTime(useEstimatedTime);
	}
	
	public void updateValueExampleCallback( float val, object hash ){
		// Hashtable h = hash as Hashtable;
		// Debug.Log("message:"+h["message"]+" val:"+val);
		Vector3 tmp = ltLogo.transform.eulerAngles;
		tmp.y = val;
		ltLogo.transform.eulerAngles = tmp;
	}
	
	public void delayedCallExample(){
		Debug.Log("delayedCallExample");
		
		LeanTween.delayedCall(0.5f, delayedCallExampleCallback).setUseEstimatedTime(useEstimatedTime);
	}
	
	public void delayedCallExampleCallback(){
		Debug.Log("Delayed function was called");
		Vector3 currentScale = ltLogo.transform.localScale;

		LeanTween.scale( ltLogo, new Vector3(currentScale.x-0.2f,currentScale.y-0.2f,currentScale.z-0.2f), 0.5f ).setEase(LeanTweenType.easeInOutCirc).setUseEstimatedTime(useEstimatedTime);
	}

	public void alphaExample(){
		Debug.Log("alphaExample");
		
		GameObject lChar = GameObject.Find ("LCharacter");
		LeanTween.alpha( lChar, 0.0f, 0.5f ).setUseEstimatedTime(useEstimatedTime);
		LeanTween.alpha( lChar, 1.0f, 0.5f ).setDelay(0.5f).setUseEstimatedTime(useEstimatedTime);
	}

	public void moveLocalExample(){
		Debug.Log("moveLocalExample");
		
		GameObject lChar = GameObject.Find ("LCharacter");
		Vector3 origPos = lChar.transform.localPosition;
		LeanTween.moveLocal( lChar, new Vector3(0.0f,2.0f,0.0f), 0.5f ).setUseEstimatedTime(useEstimatedTime);
		LeanTween.moveLocal( lChar, origPos, 0.5f ).setDelay(0.5f).setUseEstimatedTime(useEstimatedTime);
	}

	public void rotateAroundExample(){
		Debug.Log("rotateAroundExample");
		
		GameObject lChar = GameObject.Find ("LCharacter");
		LeanTween.rotateAround( lChar, Vector3.up, 360.0f, 1.0f ).setUseEstimatedTime(useEstimatedTime);
	}

	public void loopPause(){
		GameObject cube1 = GameObject.Find("Cube1");
		LeanTween.pause(cube1);
	}

	public void loopResume(){
		GameObject cube1 = GameObject.Find("Cube1");
		LeanTween.resume(cube1 );
	}

	public void punchTest(){
		LeanTween.moveX( ltLogo, 7.0f, 1.0f ).setEase(LeanTweenType.punch).setUseEstimatedTime(useEstimatedTime);
	}
}
