using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/*Tells a cut-scene to begin playing, and then load a scene when completed.*/

public class VideoController : MonoBehaviour
{

    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] string whichLevel;
    [SerializeField] GameObject activateObjectAfterPlaying;
    public long playerCurrentFrame;
    public long playerFrameCount;

    void Start()
    {
        //Begin invoke!
        InvokeRepeating("CheckOver", .1f, .1f);
    }

    private void CheckOver()
    {
        playerCurrentFrame = videoPlayer.frame;
        playerFrameCount = (int)videoPlayer.frameCount;

        if (playerCurrentFrame != 0 && playerFrameCount != 0)
        {
            if (playerCurrentFrame >= playerFrameCount - 1)
            {
                if (activateObjectAfterPlaying != null)
                {
                    activateObjectAfterPlaying.SetActive(true);
                    gameObject.SetActive(false);
                }
                else if (whichLevel != "")
                {
                    SceneManager.LoadScene(whichLevel);
                }

                //Cancel Invoke since video is no longer playing
                CancelInvoke("checkOver");
            }
        }
    }

}
