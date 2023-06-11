using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class EndingPlayer : MonoBehaviour
{
    PlayableDirector dir;
    public static Ending currentEnding;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private AudioClip[] typeSounds;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        dir = GetComponent<PlayableDirector>();

        title.text = "";
        description.text = "";

        if (currentEnding != null)
            dir.playableAsset = currentEnding.cutscene;

        dir.Play();
    }

    public void DisplayEndingName()
    {
        StartCoroutine("TypeText");
    }


    IEnumerator TypeText()
    {
        WaitForSeconds charWait = new WaitForSeconds(.06f);
        WaitForSeconds lineWait = new WaitForSeconds(1.3f);
        WaitForSeconds dotWait = new WaitForSeconds(.7f);

        foreach (char c in currentEnding.title)
        {
            title.text += c;
            source.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));

            if (c == '.')
                yield return dotWait;
            else
                yield return charWait;
        }
        yield return lineWait;

        foreach (char c in currentEnding.description)
        {
            description.text += c;
            source.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));

            if (c == '.')
                yield return dotWait;
            else
                yield return charWait;
        }

        yield return lineWait;
        yield return lineWait;
        SceneManager.LoadScene("LoadingScreen");
    }

    public void ReturnMenu()
    {
        StartCoroutine("ReturnText");
    }


    IEnumerator ReturnText()
    {
        WaitForSeconds charWait = new WaitForSeconds(.06f);
        WaitForSeconds lineWait = new WaitForSeconds(1.3f);
        WaitForSeconds dotWait = new WaitForSeconds(.7f);

        foreach (char c in currentEnding.title)
        {
            title.text += c;
            source.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));

            if (c == '.')
                yield return dotWait;
            else
                yield return charWait;
        }
        yield return lineWait;

        foreach (char c in currentEnding.description)
        {
            description.text += c;
            source.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));

            if (c == '.')
                yield return dotWait;
            else
                yield return charWait;
        }

        yield return lineWait;
        yield return lineWait;
        SceneManager.LoadScene("Menu");
    }

    public void ToDemoLevel()
    {
        StartCoroutine("DemoLevel");
    }


    IEnumerator DemoLevel()
    {
        WaitForSeconds charWait = new WaitForSeconds(.06f);
        WaitForSeconds lineWait = new WaitForSeconds(1.3f);
        WaitForSeconds dotWait = new WaitForSeconds(.7f);

        foreach (char c in currentEnding.title)
        {
            title.text += c;
            source.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));

            if (c == '.')
                yield return dotWait;
            else
                yield return charWait;
        }
        yield return lineWait;

        foreach (char c in currentEnding.description)
        {
            description.text += c;
            source.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));

            if (c == '.')
                yield return dotWait;
            else
                yield return charWait;
        }

        yield return lineWait;
        yield return lineWait;
        SceneManager.LoadScene("DemoLevel");
    }
}
