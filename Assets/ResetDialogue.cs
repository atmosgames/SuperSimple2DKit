using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDialogue : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponentInParent<DialogueTrigger>().ResetDialogue();
        gameObject.SetActive(false);
    }
}
