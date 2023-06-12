using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing246 : MonoBehaviour {

    public float gameScale = 1f;

    //private float nextElapsed = 0f;

    public GameObject tweenAlpha;

 //   public void Start(){
 //       LeanTween.alpha(tweenAlpha, 0f, 1f).setRecursive(false);   
 //   }
    
	//void Update () {
 //       if (Time.time >= nextElapsed)
 //       {
 //           nextElapsed = Time.time + 0.1f;
 //           GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

 //           float randRad = Random.Range(0f, 2f * Mathf.PI);
 //           float radius = 4f;
 //           var pos = new Vector3(Mathf.Cos(randRad ) * radius, Mathf.Sin(randRad) * radius, 0f);
 //           LeanTween.move(cube, pos, 1f).setSpeed(1f).setDestroyOnComplete(true);
 //       }

 //       Time.timeScale = gameScale;
	//}

    void Start()
    {
        //LeanTween.rotateLocal(tweenAlpha, new Vector3(0f,0f,360f), 1f).setRepeat(-1);
        //LeanTween.rotateAroundLocal(tweenAlpha, Vector3.forward, 360f, 1f).setOnComplete(()=>{
        //    LeanTween.cancel(tweenAlpha);

        //});
        LeanTween.moveX(tweenAlpha, 10f, 0f);
    }

}
