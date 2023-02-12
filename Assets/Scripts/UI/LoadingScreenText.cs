using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LoadingScreenText : MonoBehaviour
{


    string[] text = new string[]{   "> Restarting simulation....",
                                    "> Loading world4526.vw....",
                                    "> Modifying test subjects...."};

    [SerializeField] private AudioClip[] typeSounds;

    TextMeshProUGUI textMesh;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        StartCoroutine("TypeText");
    }


    IEnumerator TypeText()
    {
        WaitForSeconds charWait = new WaitForSeconds(.06f);
        WaitForSeconds lineWait = new WaitForSeconds(1.3f);
        WaitForSeconds dotWait = new WaitForSeconds(.4f);

        foreach (string str in text)
        { 
            foreach (char c in str)
            {
                textMesh.text += c;
                audioSource.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));
                
                if(c == '.')
                    yield return dotWait;
                else
                    yield return charWait;
            }
            textMesh.text += '\n';
            yield return lineWait;
        }
    }
}
