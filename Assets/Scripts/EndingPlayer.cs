using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndingPlayer : MonoBehaviour
{
    PlayableDirector dir;

    private void Awake()
    {
        dir = GetComponent<PlayableDirector>();
        dir.playableAsset = GameManager.currentEnding.cutscene;
    }
}
