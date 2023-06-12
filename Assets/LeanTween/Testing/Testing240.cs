using UnityEngine;
using System.Collections;
using DentedPixel;

public class Testing240 : MonoBehaviour {

    public GameObject cube1;
    public GameObject cube2;
    public RectTransform rect1;

	public GameObject sprite2;

	// Use this for initialization
	void Start () {
        LeanTween.moveY(cube1, cube1.transform.position.y - 15.0f, 10f).setEase(LeanTweenType.easeInQuad).setDestroyOnComplete(false).setOnComplete(()=>{
            Debug.Log("Done");
        });

		Vector3 before = cube1.transform.position;
		LeanTween.rotateAround(cube1, Vector3.forward, 360.0f, 10f).setOnComplete( ()=>{
			Debug.Log("before:"+before+" after :"+cube1.transform.position);
		});

        LeanTween.value(gameObject, new Vector3(1f,1f,1f), new Vector3(10f,10f,10f), 1f).setOnUpdate( ( Vector3 val )=>{
//            Debug.Log("val:"+val);
        });

        LeanTween.value(gameObject, ScaleGroundColor, new Color(1f, 0f, 0f, 0.2f), Color.blue, 2f).setEaseInOutBounce();

        LeanTween.scale(cube2, Vector3.one * 2f, 1f).setEasePunch().setScale(5f);

        LeanTween.scale(rect1, Vector3.one * 2f, 1f).setEasePunch().setScale(-1f);

		Vector3[] path = new Vector3[] {
			Vector2.zero,
			Vector2.zero,
			new Vector2 (1, -.5f),
			new Vector2 (1.4f, 0),
			new Vector2 (1, .5f),
			Vector2.zero,
			new Vector2 (-1, -.5f),
			new Vector2 (-1.4f, 0),
			new Vector2 (-1, .5f),
			Vector2.zero,
			Vector2.zero
		};

		LeanTween.moveSplineLocal(sprite2,path,4f)
			.setOrientToPath2d(true).setRepeat(-1);

//		int tweenId = LeanTween.move (gameObject, new Vector3 (4f, 4f, 4f), 1f).setUseManualTime (true).id;

		// Later
//		LTDescr d = LeanTween.description( tweenId );
//		d.setTime = 0.2f;
	}

    public static void ScaleGroundColor(Color to)
    {
//        Debug.Log("Color col:"+to);
        RenderSettings.ambientGroundColor = to;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
