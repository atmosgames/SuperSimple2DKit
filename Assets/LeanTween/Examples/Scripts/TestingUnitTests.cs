using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DentedPixel;

namespace DentedPixel.LTExamples
{

    public class TestingUnitTests : MonoBehaviour
    {

        public GameObject cube1;
        public GameObject cube2;
        public GameObject cube3;
        public GameObject cube4;
        public GameObject cubeAlpha1;
        public GameObject cubeAlpha2;


        private bool eventGameObjectWasCalled = false, eventGeneralWasCalled = false;
        private int lt1Id;
        private LTDescr lt2;
        private LTDescr lt3;
        private LTDescr lt4;
        private LTDescr[] groupTweens;
        private GameObject[] groupGOs;
        private int groupTweensCnt;
        private int rotateRepeat;
        private int rotateRepeatAngle;
        private GameObject boxNoCollider;
        private float timeElapsedNormalTimeScale;
        private float timeElapsedIgnoreTimeScale;
        private bool pauseTweenDidFinish = false;

        void Awake()
        {
            boxNoCollider = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(boxNoCollider.GetComponent(typeof(BoxCollider)) as Component);
        }

        void Start()
        {
            //          Time.timeScale = 0.25f;

            LeanTest.timeout = 46f;
            LeanTest.expected = 63;

            LeanTween.init(1300);

            // add a listener
            LeanTween.addListener(cube1, 0, eventGameObjectCalled);

            LeanTest.expect(LeanTween.isTweening() == false, "NOTHING TWEEENING AT BEGINNING");

            LeanTest.expect(LeanTween.isTweening(cube1) == false, "OBJECT NOT TWEEENING AT BEGINNING");

            LeanTween.scaleX(cube4, 2f, 0f).setOnComplete(() => {
                LeanTest.expect(cube4.transform.localScale.x == 2f, "TWEENED WITH ZERO TIME");
            });

            // dispatch event that is received
            LeanTween.dispatchEvent(0);
            LeanTest.expect(eventGameObjectWasCalled, "EVENT GAMEOBJECT RECEIVED");

            // do not remove listener
            LeanTest.expect(LeanTween.removeListener(cube2, 0, eventGameObjectCalled) == false, "EVENT GAMEOBJECT NOT REMOVED");
            // remove listener
            LeanTest.expect(LeanTween.removeListener(cube1, 0, eventGameObjectCalled), "EVENT GAMEOBJECT REMOVED");

            // add a listener
            LeanTween.addListener(1, eventGeneralCalled);

            // dispatch event that is received
            LeanTween.dispatchEvent(1);
            LeanTest.expect(eventGeneralWasCalled, "EVENT ALL RECEIVED");

            // remove listener
            LeanTest.expect(LeanTween.removeListener(1, eventGeneralCalled), "EVENT ALL REMOVED");

            lt1Id = LeanTween.move(cube1, new Vector3(3f, 2f, 0.5f), 1.1f).id;
            LeanTween.move(cube2, new Vector3(-3f, -2f, -0.5f), 1.1f);

            LeanTween.reset();

            // Queue up a bunch of tweens, cancel some of them but expect the remainder to finish
            GameObject[] cubes = new GameObject[99];
            int[] tweenIds = new int[cubes.Length];
            for (int i = 0; i < cubes.Length; i++)
            {
                GameObject c = cubeNamed("cancel" + i);
                tweenIds[i] = LeanTween.moveX(c, 100f, 1f).id;
                cubes[i] = c;
            }
            int onCompleteCount = 0;
            LeanTween.delayedCall(cubes[0], 0.2f, () => {
                for (int i = 0; i < cubes.Length; i++)
                {
                    if (i % 3 == 0)
                    {
                        LeanTween.cancel(cubes[i]);
                    }
                    else if (i % 3 == 1)
                    {
                        LeanTween.cancel(tweenIds[i]);
                    }
                    else if (i % 3 == 2)
                    {
                        LTDescr descr = LeanTween.descr(tweenIds[i]);
                        //                      Debug.Log("descr:"+descr);
                        descr.setOnComplete(() => {
                            onCompleteCount++;
                            //                          Debug.Log("onCompleteCount:"+onCompleteCount);
                            if (onCompleteCount >= 33)
                            {
                                LeanTest.expect(true, "CANCELS DO NOT EFFECT FINISHING");
                            }
                        });
                    }
                }
            });

            Vector3[] splineArr = new Vector3[] { new Vector3(-1f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(4f, 0f, 0f), new Vector3(20f, 0f, 0f), new Vector3(30f, 0f, 0f) };
            LTSpline cr = new LTSpline(splineArr);
            cr.place(cube4.transform, 0.5f);
            LeanTest.expect((Vector3.Distance(cube4.transform.position, new Vector3(10f, 0f, 0f)) <= 0.7f), "SPLINE POSITIONING AT HALFWAY", "position is:" + cube4.transform.position + " but should be:(10f,0f,0f)");
            LeanTween.color(cube4, Color.green, 0.01f);

            //          Debug.Log("Point 2:"+cr.ratioAtPoint(splineArr[2]));

            // OnStart Speed Test for ignoreTimeScale vs normal timeScale

            GameObject cubeDest = cubeNamed("cubeDest");
            Vector3 cubeDestEnd = new Vector3(100f, 20f, 0f);
            LeanTween.move(cubeDest, cubeDestEnd, 0.7f);

            GameObject cubeToTrans = cubeNamed("cubeToTrans");
            LeanTween.move(cubeToTrans, cubeDest.transform, 1.2f).setEase(LeanTweenType.easeOutQuad).setOnComplete(() => {
                LeanTest.expect(cubeToTrans.transform.position == cubeDestEnd, "MOVE TO TRANSFORM WORKS");
            });

            GameObject cubeDestroy = cubeNamed("cubeDestroy");
            LeanTween.moveX(cubeDestroy, 200f, 0.05f).setDelay(0.02f).setDestroyOnComplete(true);
            LeanTween.moveX(cubeDestroy, 200f, 0.1f).setDestroyOnComplete(true).setOnComplete(() => {
                LeanTest.expect(true, "TWO DESTROY ON COMPLETE'S SUCCEED");
            });

            GameObject cubeSpline = cubeNamed("cubeSpline");
            LeanTween.moveSpline(cubeSpline, new Vector3[] { new Vector3(0.5f, 0f, 0.5f), new Vector3(0.75f, 0f, 0.75f), new Vector3(1f, 0f, 1f), new Vector3(1f, 0f, 1f) }, 0.1f).setOnComplete(() => {
                LeanTest.expect(Vector3.Distance(new Vector3(1f, 0f, 1f), cubeSpline.transform.position) < 0.01f, "SPLINE WITH TWO POINTS SUCCEEDS");
            });

            // This test works when it is positioned last in the test queue (probably worth fixing when you have time)
            GameObject jumpCube = cubeNamed("jumpTime");
            jumpCube.transform.position = new Vector3(100f, 0f, 0f);
            jumpCube.transform.localScale *= 100f;
            int jumpTimeId = LeanTween.moveX(jumpCube, 200f, 1f).id;

            LeanTween.delayedCall(gameObject, 0.2f, () => {
                LTDescr d = LeanTween.descr(jumpTimeId);
                float beforeX = jumpCube.transform.position.x;
                d.setTime(0.5f);
                LeanTween.delayedCall(0.0f, () => { }).setOnStart(() => {
                    float diffAmt = 1f;// This variable is dependent on a good frame-rate because it evalutes at the next Update
                    beforeX += Time.deltaTime * 100f * 2f;
                    LeanTest.expect(Mathf.Abs(jumpCube.transform.position.x - beforeX) < diffAmt, "CHANGING TIME DOESN'T JUMP AHEAD", "Difference:" + Mathf.Abs(jumpCube.transform.position.x - beforeX) + " beforeX:" + beforeX + " now:" + jumpCube.transform.position.x + " dt:" + Time.deltaTime);
                });
            });

            // Tween with time of zero is needs to be set to it's final value
            GameObject zeroCube = cubeNamed("zeroCube");
            LeanTween.moveX(zeroCube, 10f, 0f).setOnComplete(() => {
                LeanTest.expect(zeroCube.transform.position.x == 10f, "ZERO TIME FINSHES CORRECTLY", "final x:" + zeroCube.transform.position.x);
            });

            // Scale, and OnStart
            GameObject cubeScale = cubeNamed("cubeScale");
            LeanTween.scale(cubeScale, new Vector3(5f, 5f, 5f), 0.01f).setOnStart(() => {
                LeanTest.expect(true, "ON START WAS CALLED");
            }).setOnComplete(() => {
                LeanTest.expect(cubeScale.transform.localScale.z == 5f, "SCALE", "expected scale z:" + 5f + " returned:" + cubeScale.transform.localScale.z);
            });

            // Rotate
            GameObject cubeRotate = cubeNamed("cubeRotate");
            LeanTween.rotate(cubeRotate, new Vector3(0f, 180f, 0f), 0.02f).setOnComplete(() => {
                LeanTest.expect(cubeRotate.transform.eulerAngles.y == 180f, "ROTATE", "expected rotate y:" + 180f + " returned:" + cubeRotate.transform.eulerAngles.y);
            });

            // RotateAround
            GameObject cubeRotateA = cubeNamed("cubeRotateA");
            LeanTween.rotateAround(cubeRotateA, Vector3.forward, 90f, 0.3f).setOnComplete(() => {
                LeanTest.expect(cubeRotateA.transform.eulerAngles.z == 90f, "ROTATE AROUND", "expected rotate z:" + 90f + " returned:" + cubeRotateA.transform.eulerAngles.z);
            });

            // RotateAround 360
            GameObject cubeRotateB = cubeNamed("cubeRotateB");
            cubeRotateB.transform.position = new Vector3(200f, 10f, 8f);
            LeanTween.rotateAround(cubeRotateB, Vector3.forward, 360f, 0.3f).setPoint(new Vector3(5f, 3f, 2f)).setOnComplete(() => {
                LeanTest.expect(cubeRotateB.transform.position.ToString() == (new Vector3(200f, 10f, 8f)).ToString(), "ROTATE AROUND 360", "expected rotate pos:" + (new Vector3(200f, 10f, 8f)) + " returned:" + cubeRotateB.transform.position);
            });

            // Alpha, onUpdate with passing value, onComplete value
            LeanTween.alpha(cubeAlpha1, 0.5f, 0.1f).setOnUpdate((float val) => {
                LeanTest.expect(val != 0f, "ON UPDATE VAL");
            }).setOnCompleteParam("Hi!").setOnComplete((object completeObj) => {
                LeanTest.expect(((string)completeObj) == "Hi!", "ONCOMPLETE OBJECT");
                LeanTest.expect(cubeAlpha1.GetComponent<Renderer>().material.color.a == 0.5f, "ALPHA");
            });
            // Color
            float onStartTime = -1f;
            LeanTween.color(cubeAlpha2, Color.cyan, 0.3f).setOnComplete(() => {
                LeanTest.expect(cubeAlpha2.GetComponent<Renderer>().material.color == Color.cyan, "COLOR");
                LeanTest.expect(onStartTime >= 0f && onStartTime < Time.time, "ON START", "onStartTime:" + onStartTime + " time:" + Time.time);
            }).setOnStart(() => {
                onStartTime = Time.time;
            });
            // moveLocalY (make sure uses y values)
            Vector3 beforePos = cubeAlpha1.transform.position;
            LeanTween.moveY(cubeAlpha1, 3f, 0.2f).setOnComplete(() => {
                LeanTest.expect(cubeAlpha1.transform.position.x == beforePos.x && cubeAlpha1.transform.position.z == beforePos.z, "MOVE Y");
            });

            Vector3 beforePos2 = cubeAlpha2.transform.localPosition;
            LeanTween.moveLocalZ(cubeAlpha2, 12f, 0.2f).setOnComplete(() => {
                LeanTest.expect(cubeAlpha2.transform.localPosition.x == beforePos2.x && cubeAlpha2.transform.localPosition.y == beforePos2.y, "MOVE LOCAL Z", "ax:" + cubeAlpha2.transform.localPosition.x + " bx:" + beforePos.x + " ay:" + cubeAlpha2.transform.localPosition.y + " by:" + beforePos2.y);
            });

            AudioClip audioClip = LeanAudio.createAudio(new AnimationCurve(new Keyframe(0f, 1f, 0f, -1f), new Keyframe(1f, 0f, -1f, 0f)), new AnimationCurve(new Keyframe(0f, 0.001f, 0f, 0f), new Keyframe(1f, 0.001f, 0f, 0f)), LeanAudio.options());
            LeanTween.delayedSound(gameObject, audioClip, new Vector3(0f, 0f, 0f), 0.1f).setDelay(0.2f).setOnComplete(() => {
                LeanTest.expect(Time.time > 0, "DELAYED SOUND");
            });

            // Easing Methods
            int totalEasingCheck = 0;
            int totalEasingCheckSuccess = 0;
            for (int j = 0; j < 2; j++)
            {
                bool isCheckingFrom = j == 1;
                int totalTweenTypeLength = (int)LeanTweenType.easeShake;
                for (int i = (int)LeanTweenType.notUsed; i < totalTweenTypeLength; i++)
                {
                    LeanTweenType easeType = (LeanTweenType)i;
                    GameObject cube = cubeNamed("cube" + easeType);
                    LTDescr descr = LeanTween.moveLocalX(cube, 5, 0.1f).setOnComplete((object obj) => {
                        GameObject cubeIn = obj as GameObject;
                        totalEasingCheck++;
                        if (cubeIn.transform.position.x == 5f)
                        {
                            totalEasingCheckSuccess++;
                        }
                        if (totalEasingCheck == (2 * totalTweenTypeLength))
                        {
                            LeanTest.expect(totalEasingCheck == totalEasingCheckSuccess, "EASING TYPES");
                        }
                    }).setOnCompleteParam(cube);

                    if (isCheckingFrom)
                        descr.setFrom(-5f);
                }
            }

            // value2
            bool value2UpdateCalled = false;
            LeanTween.value(gameObject, new Vector2(0, 0), new Vector2(256, 96), 0.1f).setOnUpdate((Vector2 value) => {
                value2UpdateCalled = true;
            });
            LeanTween.delayedCall(0.2f, () => {
                LeanTest.expect(value2UpdateCalled, "VALUE2 UPDATE");
            });

            // Testing specific cancellation
            GameObject cubeCancelA = cubeNamed("cubeCancelA");
            cubeCancelA.LeanMoveX(10f, 1f);
            cubeCancelA.LeanMoveY(10f, 1f);
            LeanTween.cancel(cubeCancelA, false, TweenAction.MOVE_X);
            LTDescr[] descrs = LeanTween.descriptions(cubeCancelA);
            LeanTest.expect(descrs.Length == 1, "SPECIFIC TWEEN DID CANCEL");


            // check descr
            //          LTDescr descr2 = LeanTween.descr( descrId );
            //          LeanTest.expect(descr2 == null,"DESCRIPTION STARTS AS NULL");

            StartCoroutine(timeBasedTesting());
        }

        private GameObject cubeNamed(string name)
        {
            GameObject cube = Instantiate(boxNoCollider) as GameObject;
            cube.name = name;
            return cube;
        }

        IEnumerator timeBasedTesting()
        {
            yield return new WaitForEndOfFrame();

            GameObject cubeNormal = cubeNamed("normalTimeScale");
            // float timeElapsedNormal = Time.time;
            LeanTween.moveX(cubeNormal, 12f, 1.5f).setIgnoreTimeScale(false).setOnComplete(() => {
                timeElapsedNormalTimeScale = Time.time;
            });

            LTDescr[] descr = LeanTween.descriptions(cubeNormal);
            LeanTest.expect(descr.Length >= 0 && descr[0].to.x == 12f, "WE CAN RETRIEVE A DESCRIPTION");

            GameObject cubeIgnore = cubeNamed("ignoreTimeScale");
            LeanTween.moveX(cubeIgnore, 5f, 1.5f).setIgnoreTimeScale(true).setOnComplete(() => {
                timeElapsedIgnoreTimeScale = Time.time;
            });

            yield return new WaitForSeconds(1.5f);
            LeanTest.expect(Mathf.Abs(timeElapsedNormalTimeScale - timeElapsedIgnoreTimeScale) < 0.7f, "START IGNORE TIMING", "timeElapsedIgnoreTimeScale:" + timeElapsedIgnoreTimeScale + " timeElapsedNormalTimeScale:" + timeElapsedNormalTimeScale);

            //          yield return new WaitForSeconds(100f);
            Time.timeScale = 4f;

            int pauseCount = 0;
            LeanTween.value(gameObject, 0f, 1f, 1f).setOnUpdate((float val) => {
                pauseCount++;
            }).pause();

            // Bezier should end at exact end position not just 99% close to it
            Vector3[] roundCirc = new Vector3[] { new Vector3(0f, 0f, 0f), new Vector3(-9.1f, 25.1f, 0f), new Vector3(-1.2f, 15.9f, 0f), new Vector3(-25f, 25f, 0f), new Vector3(-25f, 25f, 0f), new Vector3(-50.1f, 15.9f, 0f), new Vector3(-40.9f, 25.1f, 0f), new Vector3(-50f, 0f, 0f), new Vector3(-50f, 0f, 0f), new Vector3(-40.9f, -25.1f, 0f), new Vector3(-50.1f, -15.9f, 0f), new Vector3(-25f, -25f, 0f), new Vector3(-25f, -25f, 0f), new Vector3(0f, -15.9f, 0f), new Vector3(-9.1f, -25.1f, 0f), new Vector3(0f, 0f, 0f) };
            GameObject cubeRound = cubeNamed("bRound");
            Vector3 onStartPos = cubeRound.transform.position;
            LeanTween.moveLocal(cubeRound, roundCirc, 0.5f).setOnComplete(() => {
                LeanTest.expect(cubeRound.transform.position == onStartPos, "BEZIER CLOSED LOOP SHOULD END AT START", "onStartPos:" + onStartPos + " onEnd:" + cubeRound.transform.position);
            });

            // should be able to retrieve a point
            LTBezierPath roundCircPath = new LTBezierPath(roundCirc);
            float ratioPoint = roundCircPath.ratioAtPoint(new Vector3(-25f, 25f, 0f));
            LeanTest.expect(Mathf.Equals(ratioPoint, 0.25f), "BEZIER RATIO POINT");

            // Spline should end at exact end position not just 99% close to it
            Vector3[] roundSpline = new Vector3[] { new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), new Vector3(2f, 0f, 0f), new Vector3(0.9f, 2f, 0f), new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f) };
            GameObject cubeSpline = cubeNamed("bSpline");
            Vector3 onStartPosSpline = cubeSpline.transform.position;
            LeanTween.moveSplineLocal(cubeSpline, roundSpline, 0.5f).setOnComplete(() => {
                LeanTest.expect(Vector3.Distance(onStartPosSpline, cubeSpline.transform.position) <= 0.01f, "SPLINE CLOSED LOOP SHOULD END AT START", "onStartPos:" + onStartPosSpline + " onEnd:" + cubeSpline.transform.position + " dist:" + Vector3.Distance(onStartPosSpline, cubeSpline.transform.position));
            });

            // Sequence test, do three tweens and make sure they end at the right points
            GameObject cubeSeq = cubeNamed("cSeq");
            var seq = LeanTween.sequence().append(LeanTween.moveX(cubeSeq, 100f, 0.2f));
            seq.append(0.1f).append(LeanTween.scaleX(cubeSeq, 2f, 0.1f));
            seq.append(() => {
                LeanTest.expect(cubeSeq.transform.position.x == 100f, "SEQ MOVE X FINISHED", "move x:" + cubeSeq.transform.position.x);
                LeanTest.expect(cubeSeq.transform.localScale.x == 2f, "SEQ SCALE X FINISHED", "scale x:" + cubeSeq.transform.localScale.x);
            }).setScale(0.2f);

            // Bounds check
            GameObject cubeBounds = cubeNamed("cBounds");
            bool didPassBounds = true;
            Vector3 failPoint = Vector3.zero;
            LeanTween.move(cubeBounds, new Vector3(10, 10, 10), 0.1f).setOnUpdate((float val) => {
                //Debug.LogWarning("cubeBounds x:"+cubeBounds.transform.position.x + " y:"+ cubeBounds.transform.position.y+" z:"+cubeBounds.transform.position.z);
                if (cubeBounds.transform.position.x < 0f || cubeBounds.transform.position.x > 10f || cubeBounds.transform.position.y < 0f || cubeBounds.transform.position.y > 10f || cubeBounds.transform.position.z < 0f || cubeBounds.transform.position.z > 10f)
                {
                    didPassBounds = false;
                    failPoint = cubeBounds.transform.position;
                    //                  Debug.LogError("OUT OF BOUNDS");
                }
            }).setLoopPingPong().setRepeat(8).setOnComplete(() => {
                LeanTest.expect(didPassBounds, "OUT OF BOUNDS", "pos x:" + failPoint.x + " y:" + failPoint.y + " z:" + failPoint.z);
            });

            // Local scale check
            //GameObject cubeLocal = cubeNamed("cLocal");
            //LeanTween.scale(cubeLocal, new Vector3(0.5f, 0.5f, 0.5f), 0.2f).setOnComplete(() =>
            //{
            //    LeanTest.expect((cubeLocal.transform.localScale.x == 0.5f && cubeLocal.transform.localScale.y == 0.5f && cubeLocal.transform.localScale.z == 0.5f), "SCALE WORKS", "scale x:" + cubeLocal.transform.localScale.x + " y:" + cubeLocal.transform.localScale.y + " z:" + cubeLocal.transform.localScale.z);
            //});

            // Groups of tweens testing
            groupTweens = new LTDescr[1200];
            groupGOs = new GameObject[groupTweens.Length];
            groupTweensCnt = 0;
            int descriptionMatchCount = 0;
            for (int i = 0; i < groupTweens.Length; i++)
            {
                GameObject cube = cubeNamed("c" + i);
                cube.transform.position = new Vector3(0, 0, i * 3);

                groupGOs[i] = cube;
            }

            yield return new WaitForEndOfFrame();

            bool hasGroupTweensCheckStarted = false;
            int setOnStartNum = 0;
            int setPosNum = 0;
            bool setPosOnUpdate = true;
            for (int i = 0; i < groupTweens.Length; i++)
            {
                Vector3 finalPos = transform.position + Vector3.one * 3f;
                Dictionary<string, object> finalDict = new Dictionary<string, object> { { "final", finalPos }, { "go", groupGOs[i] } };
                groupTweens[i] = LeanTween.move(groupGOs[i], finalPos, 3f).setOnStart(() => {
                    setOnStartNum++;
                }).setOnUpdate((Vector3 newPosition) => {
                    if (transform.position.z > newPosition.z)
                    {
                        setPosOnUpdate = false;
                    }
                    //                  Debug.LogWarning("New Position: " + newPosition.ToString());
                }).
                setOnCompleteParam(finalDict).
                setOnComplete((object param) => {
                    Dictionary<string, object> finalDictRetr = param as Dictionary<string, object>;
                    Vector3 neededPos = (Vector3)finalDictRetr["final"];
                    GameObject tweenedGo = finalDictRetr["go"] as GameObject;
                    if (neededPos.ToString() == tweenedGo.transform.position.ToString())
                        setPosNum++;
                    else
                    {
                        //                      Debug.Log("neededPos:"+neededPos+" tweenedGo.transform.position:"+tweenedGo.transform.position);
                    }
                    if (hasGroupTweensCheckStarted == false)
                    {
                        hasGroupTweensCheckStarted = true;
                        LeanTween.delayedCall(gameObject, 0.1f, () => {
                            LeanTest.expect(setOnStartNum == groupTweens.Length, "SETONSTART CALLS", "expected:" + groupTweens.Length + " was:" + setOnStartNum);
                            LeanTest.expect(groupTweensCnt == groupTweens.Length, "GROUP FINISH", "expected " + groupTweens.Length + " tweens but got " + groupTweensCnt);
                            LeanTest.expect(setPosNum == groupTweens.Length, "GROUP POSITION FINISH", "expected " + groupTweens.Length + " tweens but got " + setPosNum);
                            LeanTest.expect(setPosOnUpdate, "GROUP POSITION ON UPDATE");
                        });
                    }
                    groupTweensCnt++;
                });

                if (LeanTween.description(groupTweens[i].id).trans == groupTweens[i].trans)
                    descriptionMatchCount++;
            }

            while (LeanTween.tweensRunning < groupTweens.Length)
                yield return null;

            LeanTest.expect(descriptionMatchCount == groupTweens.Length, "GROUP IDS MATCH");
            int expectedSearch = groupTweens.Length + 7;
            LeanTest.expect(LeanTween.maxSearch <= expectedSearch, "MAX SEARCH OPTIMIZED", "maxSearch:" + LeanTween.maxSearch + " should be:" + expectedSearch);
            LeanTest.expect(LeanTween.isTweening() == true, "SOMETHING IS TWEENING");

            // resume item before calling pause should continue item along it's way
            float previousXlt4 = cube4.transform.position.x;
            lt4 = LeanTween.moveX(cube4, 5.0f, 1.1f).setOnComplete(() => {
                LeanTest.expect(cube4 != null && previousXlt4 != cube4.transform.position.x, "RESUME OUT OF ORDER", "cube4:" + cube4 + " previousXlt4:" + previousXlt4 + " cube4.transform.position.x:" + (cube4 != null ? cube4.transform.position.x : 0));
            }).setDestroyOnComplete(true);
            lt4.resume();

            rotateRepeat = rotateRepeatAngle = 0;
            LeanTween.rotateAround(cube3, Vector3.forward, 360f, 0.1f).setRepeat(3).setOnComplete(rotateRepeatFinished).setOnCompleteOnRepeat(true).setDestroyOnComplete(true);
            yield return new WaitForEndOfFrame();
            LeanTween.delayedCall(0.1f * 8f + 1f, rotateRepeatAllFinished);

            int countBeforeCancel = LeanTween.tweensRunning;
            LeanTween.cancel(lt1Id);
            LeanTest.expect(countBeforeCancel == LeanTween.tweensRunning, "CANCEL AFTER RESET SHOULD FAIL", "expected " + countBeforeCancel + " but got " + LeanTween.tweensRunning);
            LeanTween.cancel(cube2);

            int tweenCount = 0;
            for (int i = 0; i < groupTweens.Length; i++)
            {
                if (LeanTween.isTweening(groupGOs[i]))
                    tweenCount++;
                if (i % 3 == 0)
                    LeanTween.pause(groupGOs[i]);
                else if (i % 3 == 1)
                    groupTweens[i].pause();
                else
                    LeanTween.pause(groupTweens[i].id);
            }
            LeanTest.expect(tweenCount == groupTweens.Length, "GROUP ISTWEENING", "expected " + groupTweens.Length + " tweens but got " + tweenCount);

            yield return new WaitForEndOfFrame();

            tweenCount = 0;
            for (int i = 0; i < groupTweens.Length; i++)
            {
                if (i % 3 == 0)
                    LeanTween.resume(groupGOs[i]);
                else if (i % 3 == 1)
                    groupTweens[i].resume();
                else
                    LeanTween.resume(groupTweens[i].id);

                if (i % 2 == 0 ? LeanTween.isTweening(groupTweens[i].id) : LeanTween.isTweening(groupGOs[i]))
                    tweenCount++;
            }
            LeanTest.expect(tweenCount == groupTweens.Length, "GROUP RESUME");

            LeanTest.expect(LeanTween.isTweening(cube1) == false, "CANCEL TWEEN LTDESCR");
            LeanTest.expect(LeanTween.isTweening(cube2) == false, "CANCEL TWEEN LEANTWEEN");

            LeanTest.expect(pauseCount == 0, "ON UPDATE NOT CALLED DURING PAUSE", "expect pause count of 0, but got " + pauseCount);


            yield return new WaitForEndOfFrame();
            Time.timeScale = 0.25f;
            float tweenTime = 0.2f;
            float expectedTime = tweenTime * (1f / Time.timeScale);
            float start = Time.realtimeSinceStartup;
            bool onUpdateWasCalled = false;
            LeanTween.moveX(cube1, -5f, tweenTime).setOnUpdate((float val) => {
                onUpdateWasCalled = true;
            }).setOnComplete(() => {
                float end = Time.realtimeSinceStartup;
                float diff = end - start;

                LeanTest.expect(Mathf.Abs(expectedTime - diff) < 0.06f, "SCALED TIMING DIFFERENCE", "expected to complete in roughly " + expectedTime + " but completed in " + diff);
                LeanTest.expect(Mathf.Approximately(cube1.transform.position.x, -5f), "SCALED ENDING POSITION", "expected to end at -5f, but it ended at " + cube1.transform.position.x);
                LeanTest.expect(onUpdateWasCalled, "ON UPDATE FIRED");
            });

            bool didGetCorrectOnUpdate = false;
            LeanTween.value(gameObject, new Vector3(1f, 1f, 1f), new Vector3(10f, 10f, 10f), 1f).setOnUpdate((Vector3 val) => {
                didGetCorrectOnUpdate = val.x >= 1f && val.y >= 1f && val.z >= 1f;
            }).setOnComplete(() => {
                LeanTest.expect(didGetCorrectOnUpdate, "VECTOR3 CALLBACK CALLED");
            });

            yield return new WaitForSeconds(expectedTime);
            Time.timeScale = 1f;

            int ltCount = 0;
            GameObject[] allGos = FindObjectsOfType(typeof(GameObject)) as GameObject[];
            foreach (GameObject go in allGos)
            {
                if (go.name == "~LeanTween")
                    ltCount++;
            }
            LeanTest.expect(ltCount == 1, "RESET CORRECTLY CLEANS UP");

            StartCoroutine(lotsOfCancels());
        }

        IEnumerator lotsOfCancels()
        {
            yield return new WaitForEndOfFrame();

            Time.timeScale = 4f;
            int cubeCount = 10;

            int[] tweensA = new int[cubeCount];
            GameObject[] aGOs = new GameObject[cubeCount];
            for (int i = 0; i < aGOs.Length; i++)
            {
                GameObject cube = Instantiate(boxNoCollider) as GameObject;
                cube.transform.position = new Vector3(0, 0, i * 2f);
                cube.name = "a" + i;
                aGOs[i] = cube;
                tweensA[i] = LeanTween.move(cube, cube.transform.position + new Vector3(10f, 0, 0), 0.5f + 1f * (1.0f / (float)aGOs.Length)).id;
                LeanTween.color(cube, Color.red, 0.01f);
            }

            yield return new WaitForSeconds(1.0f);

            int[] tweensB = new int[cubeCount];
            GameObject[] bGOs = new GameObject[cubeCount];
            for (int i = 0; i < bGOs.Length; i++)
            {
                GameObject cube = Instantiate(boxNoCollider) as GameObject;
                cube.transform.position = new Vector3(0, 0, i * 2f);
                cube.name = "b" + i;
                bGOs[i] = cube;
                tweensB[i] = LeanTween.move(cube, cube.transform.position + new Vector3(10f, 0, 0), 2f).id;
            }

            for (int i = 0; i < aGOs.Length; i++)
            {
                LeanTween.cancel(aGOs[i]);
                GameObject cube = aGOs[i];
                tweensA[i] = LeanTween.move(cube, new Vector3(0, 0, i * 2f), 2f).id;
            }

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < aGOs.Length; i++)
            {
                LeanTween.cancel(aGOs[i]);
                GameObject cube = aGOs[i];
                tweensA[i] = LeanTween.move(cube, new Vector3(0, 0, i * 2f) + new Vector3(10f, 0, 0), 2f).id;
            }

            for (int i = 0; i < bGOs.Length; i++)
            {
                LeanTween.cancel(bGOs[i]);
                GameObject cube = bGOs[i];
                tweensB[i] = LeanTween.move(cube, new Vector3(0, 0, i * 2f), 2f).id;
            }

            yield return new WaitForSeconds(2.1f);

            bool inFinalPlace = true;
            for (int i = 0; i < aGOs.Length; i++)
            {
                if (Vector3.Distance(aGOs[i].transform.position, new Vector3(0, 0, i * 2f) + new Vector3(10f, 0, 0)) > 0.1f)
                    inFinalPlace = false;
            }

            for (int i = 0; i < bGOs.Length; i++)
            {
                if (Vector3.Distance(bGOs[i].transform.position, new Vector3(0, 0, i * 2f)) > 0.1f)
                    inFinalPlace = false;
            }

            LeanTest.expect(inFinalPlace, "AFTER LOTS OF CANCELS");

            GameObject cubePaused = cubeNamed("cPaused");
            cubePaused.LeanMoveX(10f, 1f).setOnComplete(() => {
                pauseTweenDidFinish = true;
            });
            StartCoroutine(pauseTimeNow());
        }

        IEnumerator pauseTimeNow()
        {
            yield return new WaitForSeconds(0.5f);
            Time.timeScale = 0;

            LeanTween.delayedCall(0.5f, () => {
                Time.timeScale = 1f;
            }).setUseEstimatedTime(true);

            LeanTween.delayedCall(1.5f, () => {
                LeanTest.expect(pauseTweenDidFinish, "PAUSE BY TIMESCALE FINISHES");
            }).setUseEstimatedTime(true);
        }

        void rotateRepeatFinished()
        {
            if (Mathf.Abs(cube3.transform.eulerAngles.z) < 0.0001f)
                rotateRepeatAngle++;
            rotateRepeat++;
        }

        void rotateRepeatAllFinished()
        {
            LeanTest.expect(rotateRepeatAngle == 3, "ROTATE AROUND MULTIPLE", "expected 3 times received " + rotateRepeatAngle + " times");
            LeanTest.expect(rotateRepeat == 3, "ROTATE REPEAT", "expected 3 times received " + rotateRepeat + " times");
            LeanTest.expect(cube3 == null, "DESTROY ON COMPLETE", "cube3:" + cube3);
        }

        void eventGameObjectCalled(LTEvent e)
        {
            eventGameObjectWasCalled = true;
        }

        void eventGeneralCalled(LTEvent e)
        {
            eventGeneralWasCalled = true;
        }

    }

}