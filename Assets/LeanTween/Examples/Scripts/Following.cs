using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour {

    public Transform planet;

    public Transform followArrow;

    public Transform dude1;
    public Transform dude2;
    public Transform dude3;
    public Transform dude4;
    public Transform dude5;

    public Transform dude1Title;
    public Transform dude2Title;
    public Transform dude3Title;
    public Transform dude4Title;
    public Transform dude5Title;

    private Color dude1ColorVelocity;

    private Vector3 velocityPos;

    private void Start()
    {
        followArrow.gameObject.LeanDelayedCall(3f, moveArrow).setOnStart(moveArrow).setRepeat(-1);

        // Follow Local Y Position of Arrow
        LeanTween.followDamp(dude1, followArrow, LeanProp.localY, 1.1f);
        LeanTween.followSpring(dude2, followArrow, LeanProp.localY, 1.1f);
        LeanTween.followBounceOut(dude3, followArrow, LeanProp.localY, 1.1f);
        LeanTween.followSpring(dude4, followArrow, LeanProp.localY, 1.1f, -1f, 1.5f, 0.8f);
        LeanTween.followLinear(dude5, followArrow, LeanProp.localY, 50f);

        // Follow Arrow color
        LeanTween.followDamp(dude1, followArrow, LeanProp.color, 1.1f);
        LeanTween.followSpring(dude2, followArrow, LeanProp.color, 1.1f);
        LeanTween.followBounceOut(dude3, followArrow, LeanProp.color, 1.1f);
        LeanTween.followSpring(dude4, followArrow, LeanProp.color, 1.1f, -1f, 1.5f, 0.8f);
        LeanTween.followLinear(dude5, followArrow, LeanProp.color, 0.5f);

        // Follow Arrow scale
        LeanTween.followDamp(dude1, followArrow, LeanProp.scale, 1.1f);
        LeanTween.followSpring(dude2, followArrow, LeanProp.scale, 1.1f);
        LeanTween.followBounceOut(dude3, followArrow, LeanProp.scale, 1.1f);
        LeanTween.followSpring(dude4, followArrow, LeanProp.scale, 1.1f, -1f, 1.5f, 0.8f);
        LeanTween.followLinear(dude5, followArrow, LeanProp.scale, 5f);

        // Titles
        var titleOffset = new Vector3(0.0f, -20f, -18f);
        LeanTween.followDamp(dude1Title, dude1, LeanProp.localPosition, 0.6f).setOffset(titleOffset);
        LeanTween.followSpring(dude2Title, dude2, LeanProp.localPosition, 0.6f).setOffset(titleOffset);
        LeanTween.followBounceOut(dude3Title, dude3, LeanProp.localPosition, 0.6f).setOffset(titleOffset);
        LeanTween.followSpring(dude4Title, dude4, LeanProp.localPosition, 0.6f, -1f, 1.5f, 0.8f).setOffset(titleOffset);
        LeanTween.followLinear(dude5Title, dude5, LeanProp.localPosition, 30f).setOffset(titleOffset);

        // Rotate Planet
        var localPos = Camera.main.transform.InverseTransformPoint(planet.transform.position);
        LeanTween.rotateAround(Camera.main.gameObject, Vector3.left, 360f, 300f).setPoint(localPos).setRepeat(-1);
    }

    private float fromY;
    private float velocityY;
    private Vector3 fromVec3;
    private Vector3 velocityVec3;
    private Color fromColor;
    private Color velocityColor;

    private void Update()
    {
        // Use the smooth methods to follow variables in which ever manner you wish!
        fromY = LeanSmooth.spring(fromY, followArrow.localPosition.y, ref velocityY, 1.1f);
        fromVec3 = LeanSmooth.spring(fromVec3, dude5Title.localPosition, ref velocityVec3, 1.1f);
        fromColor = LeanSmooth.spring(fromColor, dude1.GetComponent<Renderer>().material.color, ref velocityColor, 1.1f);
        Debug.Log("Smoothed y:" + fromY + " vec3:" + fromVec3 + " color:" + fromColor);
    }

	private void moveArrow()
    {
        LeanTween.moveLocalY(followArrow.gameObject, Random.Range(-100f, 100f), 0f);

        var randomCol = new Color(Random.value, Random.value, Random.value);
        LeanTween.color(followArrow.gameObject, randomCol, 0f);

        var randomVal = Random.Range(5f, 10f);
        followArrow.localScale = Vector3.one * randomVal;
    }
}
