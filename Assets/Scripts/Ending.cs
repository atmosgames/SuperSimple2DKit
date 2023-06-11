using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "New Ending", menuName = "Ending")]
public class Ending : ScriptableObject
{
    public string title;
    [Multiline]
    public string description;
    public TimelineAsset cutscene;
    public Sprite icon;
}
