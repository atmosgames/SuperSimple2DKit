using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class LoadingScreenText : MonoBehaviour
{

    int numer;
    string[] text = new string[]{   "> Restarting simulation....",
                                    "",
                                    "> Modifying test subjects...."};

    bool skip = false;

    [SerializeField] private AudioClip[] typeSounds;

    TextMeshProUGUI textMesh;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();

        numer = PlayerPrefs.GetInt("nr", 548);
        numer++;
        PlayerPrefs.SetInt("nr", numer);
        text[1] = "> Loading world" + numer + ".vw....";

        StartCoroutine("TypeText");
    }

    private void Update()
    {
        if(Input.anyKey)
            skip = true;
    }

    IEnumerator TypeText()
    {
        WaitForSeconds charWait = new WaitForSeconds(.04f);
        WaitForSeconds lineWait = new WaitForSeconds(1f);
        WaitForSeconds dotWait = new WaitForSeconds(.3f);


        foreach (string str in text)
        { 
            foreach (char c in str)
            {
                textMesh.text += c;
                audioSource.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));
                
                if(skip)
                {
                    charWait = new WaitForSeconds(.008f);
                    lineWait = new WaitForSeconds(0.5f);
                    dotWait = new WaitForSeconds(.1f);
                }

                if(c == '.')
                    yield return dotWait;
                else
                    yield return charWait;
            }
            textMesh.text += '\n';
            yield return lineWait;
        }

        yield return lineWait;
        SceneManager.LoadScene("World");
    }
}
