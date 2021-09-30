using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*Controls the dialogue box and it's communication with Dialogue.cs, which contains the character dialogue*/

public class DialogueBoxController : MonoBehaviour
{

    [Header("References")]
    public Animator animator;
    public AudioSource audioSource;
    public AudioSource dialogueAudioSource;
    [SerializeField] Dialogue dialogue;
    private DialogueTrigger currentDialogueTrigger;
    private GameObject finishTalkingActivateGameObject;

    [Header("Sounds")]
    private AudioClip[] audioLines;
    private AudioClip[] audioChoices;
    [SerializeField] private AudioClip selectionSound;
    [SerializeField] private AudioClip[] typeSounds;

    [Header("Text Mesh Pro")]
    [SerializeField] TextMeshProUGUI choice1Mesh;
    [SerializeField] TextMeshProUGUI choice2Mesh;
    [SerializeField] TextMeshProUGUI nameMesh;
    [SerializeField] TextMeshProUGUI textMesh;

    [Header("Other")]
    private bool ableToAdvance;
    private bool activated;
    [SerializeField] private float typeSpeed = 1f;
    private int choiceLocation;
    private int cPos = 0;
    private string[] characterDiologue;
    private string[] choiceDiologue;
    private DialogueTrigger dialogueTrigger;
    [System.NonSerialized] public bool extendConvo;
    private string finishTalkingAnimatorBool;
    private string finishTalkingActivateGameObjectString;
    private string fileName;
    private int index = -1;
    private bool repeat;
    private bool horizontalKeyIsDown = true;
    private bool submitKeyIsDown = true;
    private bool typing = true;

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            //Submit
            //Check for key press
            if (((Input.GetAxis("Submit") > 0) || (Input.GetAxis("Jump") > 0)) && !submitKeyIsDown)
            {
                submitKeyIsDown = true;
                if (!typing)
                {
                    if (index < choiceLocation || (extendConvo && index < characterDiologue.Length - 1))
                    {
                        if (ableToAdvance)
                        {
                            StartCoroutine(Advance());
                        }
                    }
                    else
                    {
                        StartCoroutine(Close());
                    }
                    if (index == 0)
                    {
                        ableToAdvance = true;
                    }
                }
            }

            //Check for first release to ensure we can't spam
            if (submitKeyIsDown && Input.GetAxis("Submit") < .001 && Input.GetAxis("Jump") < .001)
            {
                if (!typing)
                {
                    submitKeyIsDown = false;
                    if (index == 0)
                    {
                        ableToAdvance = true;
                    }
                }
            }

            //Choices
            //Check for key press
            if ((Input.GetAxis("Horizontal") != 0) && !horizontalKeyIsDown && animator.GetBool("hasChoices") == true)
            {
                if (animator.GetInteger("choiceSelection") == 1)
                {
                    animator.SetInteger("choiceSelection", 2);
                    extendConvo = true;
                }
                else
                {
                    extendConvo = false;
                    animator.SetInteger("choiceSelection", 1);
                }
                audioSource.PlayOneShot(selectionSound);
                horizontalKeyIsDown = true;
            }

            //Check for first release to ensure we can't spam
            if (horizontalKeyIsDown && Input.GetAxis("Horizontal") == 0)
            {
                horizontalKeyIsDown = false;
            }
        }
    }

    public void Appear(string fName, string characterName, DialogueTrigger dTrigger, bool useItemAfterClose, AudioClip[] audioL, AudioClip[] audioC, string finishTalkingAnimBool, GameObject finishTalkingActivateGObject, string finishTalkingActivateGOString, bool r)
    {
        repeat = r;
        finishTalkingAnimatorBool = finishTalkingAnimBool;
        finishTalkingActivateGameObject = finishTalkingActivateGObject;
        finishTalkingActivateGameObjectString = finishTalkingActivateGOString;
        dialogueTrigger = dTrigger;
        choice1Mesh.text = "";
        choice2Mesh.text = "";
        fileName = fName;
        audioLines = audioL;
        audioChoices = audioC;

        if (useItemAfterClose)
        {
            currentDialogueTrigger = dialogueTrigger;
        }

        nameMesh.text = characterName;
        characterDiologue = dialogue.dialogue[fileName];

        if (dialogue.dialogue.ContainsKey(fileName + "Choice1"))
        {
            choiceDiologue = dialogue.dialogue[fileName + "Choice1"];
            choiceLocation = GetChoiceLocation();
        }
        else
        {
            choiceLocation = characterDiologue.Length - 1;
        }

        animator.SetBool("active", true);
        activated = true;
        NewPlayer.Instance.Freeze(true);
        StartCoroutine(Advance());
    }

    IEnumerator Close()
    {
        if (index == choiceLocation && dialogue.dialogue.ContainsKey(fileName + "Choice1") && audioChoices.Length != 0)
        {
            audioSource.Stop();
            yield return new WaitForSeconds(.1f);
            if (animator.GetInteger("choiceSelection") == 1)
            {
                dialogueAudioSource.PlayOneShot(audioChoices[0]);
            }
            else
            {
                dialogueAudioSource.PlayOneShot(audioChoices[1]);
            }
        }

        //The dialogueTrigger will pass itself into this function only if you have the right items to close the dialogue and complete the quest
        if (currentDialogueTrigger != null)
        {
            currentDialogueTrigger.UseItem();
        }

        activated = false;
        animator.SetBool("active", false);
        StopCoroutine("TypeText");
        index = -1;
        submitKeyIsDown = false;
        ableToAdvance = false;
        extendConvo = false;
        choiceLocation = 0;
        ShowChoices(false);

        if (finishTalkingAnimatorBool != "")
        {
            dialogueTrigger.GetComponent<DialogueTrigger>().useItemAnimator.SetBool(finishTalkingAnimatorBool, true);
        }

        if (finishTalkingActivateGameObject != null)
        {
            finishTalkingActivateGameObject.SetActive(true);
        }
        else if (finishTalkingActivateGameObjectString != "")
        {
            GameObject.Find(finishTalkingActivateGameObjectString).GetComponent<BoxCollider2D>().enabled = true;
        }

        if (!repeat)
        {
            dialogueTrigger.completed = true;
        }

        dialogueTrigger = null;
        finishTalkingAnimatorBool = "";
        finishTalkingActivateGameObject = null;
        finishTalkingActivateGameObjectString = "";
        yield return new WaitForSeconds(1f);
        NewPlayer.Instance.Freeze(false);
        animator.SetInteger("choiceSelection", 1);
    }

    IEnumerator Advance()
    {
        index++;
        typing = true;

        if (ableToAdvance)
        {
            animator.SetTrigger("press");
        }

        if (index != choiceLocation)
        {
            ShowChoices(false);
        }


        if (index == choiceLocation + 1 && dialogue.dialogue.ContainsKey(fileName + "Choice1") && audioChoices.Length != 0)
        {
            audioSource.Stop();
            yield return new WaitForSeconds(.1f);
            if (animator.GetInteger("choiceSelection") == 1)
            {
                dialogueAudioSource.PlayOneShot(audioChoices[0]);
                yield return new WaitForSeconds(audioChoices[0].length);
            }
            else
            {
                dialogueAudioSource.PlayOneShot(audioChoices[1]);
                yield return new WaitForSeconds(audioChoices[1].length);
            }
        }

        textMesh.text = "";
        StartCoroutine("TypeText");

        //Wait before typing
        yield return new WaitForSeconds(.4f);

        //Show choices
        if (index == choiceLocation && dialogue.dialogue.ContainsKey(fileName + "Choice1"))
        {
            ShowChoices(true);
        }

        //Play character audio
        if (audioLines.Length != 0)
        {
            dialogueAudioSource.Stop();
            if (index < audioLines.Length)
            {
                if (audioLines[index] != null)
                {
                    dialogueAudioSource.PlayOneShot(audioLines[index]);
                }
            }
        }
    }

    IEnumerator TypeText()
    {
        WaitForSeconds wait = new WaitForSeconds(.01f);
        foreach (char c in characterDiologue[index])
        {
            cPos++;
            if (cPos != 0 && cPos == characterDiologue[index].Length)
            {
                typing = false;
                cPos = 0;
            }

            textMesh.text += c;
            audioSource.PlayOneShot(typeSounds[Random.Range(0, typeSounds.Length)], Random.Range(.3f, .5f));
            yield return wait;
        }
    }

    public int GetChoiceLocation()
    {
        for (int i = 0; i < choiceDiologue.Length; i++)
        {
            if (choiceDiologue[i] != "")
            {
                return i;
            }
        }
        return 0;
    }

    void ShowChoices(bool show)
    {
        animator.SetBool("hasChoices", show);
        if (show)
        {
            choice1Mesh.text = dialogue.dialogue[fileName + "Choice1"][choiceLocation];
            choice2Mesh.text = dialogue.dialogue[fileName + "Choice2"][choiceLocation];
        }
    }
}
