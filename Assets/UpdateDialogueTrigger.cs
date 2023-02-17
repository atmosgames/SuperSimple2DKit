using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDialogueTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueA;
    [SerializeField] private string dialogueB;

    [SerializeField] private GameObject toActivateA;
    [SerializeField] private GameObject toActivateB;

    [SerializeField] private DialogueTrigger trigger;

    private void OnEnable()
    {
        trigger.SetDialgueA(dialogueA);
        trigger.SetDialgueB(dialogueB);
        trigger.SetActivateObject1(toActivateA);
        trigger.SetActivateObject2(toActivateB);
        trigger.ResetDialogue();
    }
}
