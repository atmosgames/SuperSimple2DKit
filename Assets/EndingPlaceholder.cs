using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPlaceholder : MonoBehaviour
{
    [SerializeField] private Ending ending;

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        if (PlayerPrefs.GetInt(ending.name, 0) == 1 && ending.icon)
        {
            GetComponent<Image>().sprite = ending.icon;

            if(PlayerPrefs.GetString("CurrendEnding") == ending.name)
                transform.LeanScale(Vector3.one * 0.5f, 0.5f).setEaseInOutSine().setOnComplete(() => { transform.LeanScale(Vector3.one * 1.5f, 1f).setEaseInOutSine().setLoopPingPong(); });//.setLoopPingPong();
            else
                transform.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();
        }
        else
            transform.LeanScale(Vector3.one, 0.5f).setEaseInOutExpo();//.setLoopPingPong();
    }
}
